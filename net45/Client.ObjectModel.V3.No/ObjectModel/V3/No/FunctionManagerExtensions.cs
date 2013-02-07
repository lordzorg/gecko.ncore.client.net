using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gecko.NCore.Client.Functions;

namespace Gecko.NCore.Client.ObjectModel.V3.No
{
    /// <summary>
    /// Provides strongly typed extensions to <see cref="FunctionManager"/>
    /// </summary>
    public static class FunctionManagerExtensions
    {
        /// <summary>
        /// Henter journalpost aktivitetsmaler.
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, string> HentJournalpostAktivitetsmaler(this IFunctionManager instance)
        {
            var aktivitetsmaler = (DataSet)instance.Execute("HentAktivitetsmaler", 0);
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
        public static IDictionary<int, string> HentSakAktivitetsmaler(this IFunctionManager instance)
        {
            var aktivitetsmaler = (DataSet)instance.Execute("HentAktivitetsmaler", 0);
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
        public static void OpprettJournalpostAktivitetsflyt(this IFunctionManager instance, int templateId, int journalpostId, int position, bool asSibling, IEnumerable<AvsenderMottaker> mottakere)
        {
            instance.Execute("NewActivitiesFromTemplate", templateId, journalpostId, 0, position, asSibling, string.Join(";", mottakere.Select(AktivitetMottakerToString).ToArray()));
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
        public static void OpprettSakAktivitetsflyt(this IFunctionManager instance, int templateId, int sakId, int position, bool asSibling, IEnumerable<AvsenderMottaker> mottakere)
        {
            instance.Execute("NewActivitiesFromTemplate", templateId, sakId, 1, position, asSibling, string.Join(";", mottakere.Select(AktivitetMottakerToString).ToArray()));
        }

        private static string AktivitetMottakerToString(AvsenderMottaker aktivitetMottaker)
        {
            var personId = aktivitetMottaker.SaksbehandlerId.HasValue ? aktivitetMottaker.SaksbehandlerId.Value.ToString() : "@";
            var administrativEnhetId = aktivitetMottaker.AdministrativEnhetId.HasValue ? aktivitetMottaker.AdministrativEnhetId.Value.ToString() : "@";
            return administrativEnhetId + ":" + personId;
        }

        /// <summary>
        /// Henter tilgangskodene til en bruker.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="personnavnId">The personnavn id.</param>
        /// <returns></returns>
        public static IEnumerable<string> HentTilgangskoder(this IFunctionManager instance, int personnavnId)
        {
            var tilgangskodeIds = (string)instance.Execute("Tilgangskoder", personnavnId);
            return tilgangskodeIds.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(tilgangskodeId => tilgangskodeId.Trim());
        }

        /// <summary>
        /// Hents the dokumentmaltyper.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IDictionary<int, string> HentDokumentmaltyper(this IFunctionManager instance)
        {
            var dokumentmalTyper = (DataSet) instance.Execute("HentDokumentmalTyper");
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
        public static IEnumerable<Dokumentmal> HentDokumentmal(this IFunctionManager instance, int journalpostId, int dokumentbeskrivelseId, params int[] dokumentmalTypeIds)
        {
            var dokumentmaler = (DataSet)instance.Execute("HentDokumentmaler", journalpostId, dokumentbeskrivelseId, string.Join(",", dokumentmalTypeIds.Select(x => x.ToString()).ToArray()));
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
        public static int OpprettTilgangsgruppe(this IFunctionManager instance, Sak sak, params int[] personIds)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (sak == null)
                throw new ArgumentNullException("sak");

            if (personIds == null || personIds.Length == 0)
                throw new ArgumentException(@"Det må angis minst ett medlem av tilgangsgruppen.", "personIds");

            return (int)instance.Execute("OpprettTilgangsgruppeHandler", sak.Id, 0, string.Join(",", personIds.Select(x => x.ToString()).ToArray()));
        }

        /// <summary>
        /// Opprett ad-hoc tilgangsgruppe for journalpost.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="journalpost">The journalpost.</param>
        /// <param name="personIds">The person ids.</param>
        /// <returns></returns>
        public static int OpprettTilgangsgruppe(this IFunctionManager instance, Journalpost journalpost, params int[] personIds)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (journalpost == null)
                throw new ArgumentNullException("journalpost");

            if (personIds == null || personIds.Length == 0)
                throw new ArgumentException(@"Det må angis minst ett medlem av tilgangsgruppen.", "personIds");

            return (int)instance.Execute("OpprettTilgangsgruppeHandler", 0, journalpost.Id, string.Join(",", personIds.Select(x => x.ToString()).ToArray()));
        }
    }
}
