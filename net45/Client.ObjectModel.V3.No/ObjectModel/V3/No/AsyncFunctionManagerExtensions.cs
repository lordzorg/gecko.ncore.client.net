using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Gecko.NCore.Client.Functions;

namespace Gecko.NCore.Client.ObjectModel.V3.No
{
    /// <summary>
    /// Provides strongly typed extensions to <see cref="FunctionManager"/>
    /// </summary>
    public static class AsyncFunctionManagerExtensions
    {
        /// <summary>
        /// Henter journalpost aktivitetsmaler.
        /// </summary>
        /// <returns></returns>
        public static async Task<IDictionary<int, string>> HentJournalpostAktivitetsmalerAsync(this IAsyncFunctionManager instance)
        {
            var aktivitetsmaler = (DataSet)await instance.ExecuteAsync("HentAktivitetsmaler", 0);
            var templates = new Dictionary<int, string>();
            for (var i = 0; i < aktivitetsmaler.Tables[0].DefaultView.Count; i++)
            {
                var aktivitetsmal = aktivitetsmaler.Tables[0].DefaultView[i];
                templates.Add(Convert.ToInt32(aktivitetsmal[0]), aktivitetsmal[1].ToString());
            }
            return templates;
        }

        /// <summary>
        /// Henter sak aktivitetsmaler.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static async Task<IDictionary<int, string>> HentSakAktivitetsmalerAsync(this IAsyncFunctionManager instance)
        {
            var aktivitetsmaler = (DataSet)await instance.ExecuteAsync("HentAktivitetsmaler", 0);
            var templates = new Dictionary<int, string>();
            for (var i = 0; i < aktivitetsmaler.Tables[0].DefaultView.Count; i++)
            {
                var aktivitetsmal = aktivitetsmaler.Tables[0].DefaultView[i];
                templates.Add(Convert.ToInt32(aktivitetsmal[0]), aktivitetsmal[1].ToString());
            }
            return templates;
        }

        /// <summary>
        /// Oppretts the journalpost aktivitetsflyt.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="journalpostId">The journalpost id.</param>
        /// <param name="position">The position.</param>
        /// <param name="asSibling">if set to <c>true</c> [as sibling].</param>
        /// <param name="mottakere">The mottakere.</param>
        public static async Task OpprettJournalpostAktivitetsflytAsync(this IAsyncFunctionManager instance, int templateId, int journalpostId, int position, bool asSibling, IEnumerable<AvsenderMottaker> mottakere)
        {
            await instance.ExecuteAsync("NewActivitiesFromTemplate", templateId, journalpostId, 0, position, asSibling, string.Join(";", mottakere.Select(AktivitetMottakerToString).ToArray()));
        }

        /// <summary>
        /// Oppretts the sak aktivitetsflyt.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="sakId">The sak id.</param>
        /// <param name="position">The position.</param>
        /// <param name="asSibling">if set to <c>true</c> [as sibling].</param>
        /// <param name="mottakere">The mottakere.</param>
        public static async Task OpprettSakAktivitetsflytAsync(this IAsyncFunctionManager instance, int templateId, int sakId, int position, bool asSibling, IEnumerable<AvsenderMottaker> mottakere)
        {
            await instance.ExecuteAsync("NewActivitiesFromTemplate", templateId, sakId, 1, position, asSibling, string.Join(";", mottakere.Select(AktivitetMottakerToString).ToArray()));
        }

        private static string AktivitetMottakerToString(AvsenderMottaker aktivitetMottaker)
        {
            var personId = aktivitetMottaker.SaksbehandlerId.HasValue ? aktivitetMottaker.SaksbehandlerId.Value.ToString(CultureInfo.InvariantCulture) : "@";
            var administrativEnhetId = aktivitetMottaker.AdministrativEnhetId.HasValue ? aktivitetMottaker.AdministrativEnhetId.Value.ToString(CultureInfo.InvariantCulture) : "@";
            return administrativEnhetId + ":" + personId;
        }

        /// <summary>
        /// Henter tilgangskodene til en bruker.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="personnavnId">The personnavn id.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> HentTilgangskoderAsync(this IAsyncFunctionManager instance, int personnavnId)
        {
            var tilgangskodeIds = (string)await instance.ExecuteAsync("Tilgangskoder", personnavnId);
            return tilgangskodeIds.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(tilgangskodeId => tilgangskodeId.Trim());
        }

        /// <summary>
        /// Hents the dokumentmaltyper.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static async Task<IDictionary<int, string>> HentDokumentmaltyperAsync(this IAsyncFunctionManager instance)
        {
            var dokumentmalTyper = (DataSet) await instance.ExecuteAsync("HentDokumentmalTyper");
            return dokumentmalTyper.Tables[0].Rows.Cast<DataRow>().ToDictionary(dokumentmalType => Convert.ToInt32(dokumentmalType["DMT_ID"]), dokumentmalType => dokumentmalType["DMT_BESKRIVELSE"].ToString());
        }

        /// <summary>
        /// Hents the dokumentmal.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="journalpostId">The journalpost id.</param>
        /// <param name="dokumentbeskrivelseId">The dokumentbeskrivelse id.</param>
        /// <param name="dokumentmalTypeIds">The dokumentmal type ids.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Dokumentmal>> HentDokumentmalAsync(this IAsyncFunctionManager instance, int journalpostId, int dokumentbeskrivelseId, params int[] dokumentmalTypeIds)
        {
            var dokumentmaler = (DataSet)await instance.ExecuteAsync("HentDokumentmaler", journalpostId, dokumentbeskrivelseId, string.Join(",", dokumentmalTypeIds.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray()));
            return from DataRow dokumentmalRow in dokumentmaler.Tables[0].Rows select new Dokumentmal
                                                                                          {
                                                                                              Id = Convert.ToInt32(dokumentmalRow["Id"]),
                                                                                              Betegnelse = (string) dokumentmalRow["malnavn"],
                                                                                              DokumentmalTypeId = Convert.ToInt32(dokumentmalRow["Type"])
                                                                                          };
        }

        /// <summary>
        /// Opprett ad-hoc tilgangsgruppe for sak.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="sak">The sak.</param>
        /// <param name="personIds">The person ids.</param>
        /// <returns></returns>
        public static async Task<int> OpprettTilgangsgruppeAsync(this IAsyncFunctionManager instance, Sak sak, params int[] personIds)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (sak == null)
                throw new ArgumentNullException("sak");

            if (personIds == null || personIds.Length == 0)
                throw new ArgumentException(@"Det må angis minst ett medlem av tilgangsgruppen.", "personIds");

            return (int)await instance.ExecuteAsync("OpprettTilgangsgruppeHandler", sak.Id, 0, string.Join(",", personIds.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray()));
        }

        /// <summary>
        /// Opprett ad-hoc tilgangsgruppe for journalpost.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="journalpost">The journalpost.</param>
        /// <param name="personIds">The person ids.</param>
        /// <returns></returns>
        public static async Task<int> OpprettTilgangsgruppeAsync(this IAsyncFunctionManager instance, Journalpost journalpost, params int[] personIds)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (journalpost == null)
                throw new ArgumentNullException("journalpost");

            if (personIds == null || personIds.Length == 0)
                throw new ArgumentException(@"Det må angis minst ett medlem av tilgangsgruppen.", "personIds");

            return (int)await instance.ExecuteAsync("OpprettTilgangsgruppeHandler", 0, journalpost.Id, string.Join(",", personIds.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray()));
        }
    }
}
