using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Gecko.NCore.Client.Properties;
using Gecko.NCore.Client.Functions;

namespace Gecko.NCore.Client.ObjectModel.V3.En
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
        public static IDictionary<int, string> GetRegistryEntryActivityTemplates(this IFunctionManager instance)
        {
            var aktivitetsmaler = (DataSet) instance.Execute("HentAktivitetsmaler", 1);
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
        public static IDictionary<int, string> GetCaseActivityTemplates(this IFunctionManager instance)
        {
            var aktivitetsmaler = (DataSet) instance.Execute("HentAktivitetsmaler", 0);
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
        /// <param name="registryEntryId">The journalpost id.</param>
        /// <param name="position">The position.</param>
        /// <param name="asSibling">if set to <c>true</c> [as sibling].</param>
        /// <param name="recipients">The mottakere.</param>
        public static void CreateRegistryEntryActivities(this IFunctionManager instance, int templateId,
                                                         int registryEntryId, int position, bool asSibling,
                                                         IEnumerable<SenderRecipient> recipients)
        {
            instance.Execute("NewActivitiesFromTemplate", templateId, registryEntryId, 1, position, asSibling,
                             string.Join(";", recipients.Select(RecipientToString).ToArray()));
        }

        /// <summary>
        /// Oppretts the sak aktivitetsflyt.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="caseId">The sak id.</param>
        /// <param name="position">The position.</param>
        /// <param name="asSibling">if set to <c>true</c> [as sibling].</param>
        /// <param name="recipients">The mottakere.</param>
        public static void CreateCaseActivities(this IFunctionManager instance, int templateId, int caseId, int position,
                                                bool asSibling, IEnumerable<SenderRecipient> recipients)
        {
            instance.Execute("NewActivitiesFromTemplate", templateId, caseId, 0, position, asSibling,
                             string.Join(";", recipients.Select(RecipientToString).ToArray()));
        }

        private static string RecipientToString(SenderRecipient aktivitetMottaker)
        {
            var personId = aktivitetMottaker.OfficerNameId.HasValue
                               ? aktivitetMottaker.OfficerNameId.Value.ToString(CultureInfo.InvariantCulture)
                               : "@";
            var administrativEnhetId = aktivitetMottaker.AdministrativeUnitId.HasValue
                                           ? aktivitetMottaker.AdministrativeUnitId.Value.ToString(
                                               CultureInfo.InvariantCulture)
                                           : "@";
            return administrativEnhetId + ":" + personId;
        }

        /// <summary>
        /// Henter tilgangskodene til en bruker.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userNameId">The personnavn id.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetAccessCodes(this IFunctionManager instance, int userNameId)
        {
            var tilgangskodeIds = (string) instance.Execute("Tilgangskoder", userNameId);
            return
                tilgangskodeIds.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                               .Select(tilgangskodeId => tilgangskodeId.Trim());
        }

        /// <summary>
        /// Hents the dokumentmaltyper.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IDictionary<int, string> GetDocumentTemplateTypes(this IFunctionManager instance)
        {
            var dokumentmalTyper = (DataSet) instance.Execute("HentDokumentmalTyper");
            return
                dokumentmalTyper.Tables[0].Rows.Cast<DataRow>()
                                          .ToDictionary(dokumentmalType => Convert.ToInt32(dokumentmalType["DMT_ID"]),
                                                        dokumentmalType => dokumentmalType["DMT_BESKRIVELSE"].ToString());
        }

        /// <summary>
        /// Hents the dokumentmal.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="registryEntryTypeIds">The registry entry type ids.</param>
        /// <returns></returns>
        public static IEnumerable<DocumentTemplate> GetDocumentTemplates(this IFunctionManager instance,
                                                                         int registryEntryId, int documentDescriptionId,
                                                                         params string[] registryEntryTypeIds)
        {
            var documentTemplateSet =
                (DataSet)
                instance.Execute("HentDokumentmaler", registryEntryId, documentDescriptionId,
                                 string.Join(",", registryEntryTypeIds));
            var documentTemplates = from DataRow dokumentmalRow in documentTemplateSet.Tables[0].Rows
                                    select new DocumentTemplate
                                               {
                                                   Id = Convert.ToInt32(dokumentmalRow["Id"]),
                                                   Description = (string) dokumentmalRow["malnavn"],
                                                   DocumentTemplateTypeId = Convert.ToInt32(dokumentmalRow["Type"])
                                               };
            return documentTemplates.OrderBy(x => x.FileName);
        }

        /// <summary>
        /// Gets the document templates.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="documentTemplateTypeId">The document template type id.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="registryEntryTypeIds">The registry entry type ids.</param>
        /// <returns></returns>
        public static IEnumerable<DocumentTemplate> GetDocumentTemplates(this IFunctionManager instance,
                                                                         int documentTemplateTypeId, int registryEntryId,
                                                                         int documentDescriptionId,
                                                                         params string[] registryEntryTypeIds)
        {
            var documentTemplates = instance.GetDocumentTemplates(registryEntryId, documentDescriptionId,
                                                                  registryEntryTypeIds);
            return documentTemplates.Where(x => x.DocumentTemplateTypeId == documentTemplateTypeId);
        }

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static IEnumerable<UserRole> GetUserRoles(this IFunctionManager instance, int userId)
        {
            var functionResult = (string) instance.Execute("HentRolleHandler", userId);
            foreach (var roleElement in functionResult.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries))
            {
                var roleValues = roleElement.Split(',');
                var userRole = new UserRole
                                   {
                                       Id = int.Parse(roleValues[0]),
                                       RoleId = int.Parse(roleValues[1]),
                                       DefaultRole = int.Parse(roleValues[2]) == -1,
                                       Title = roleValues[3],
                                       AdministrativeUnitId = int.Parse(roleValues[4])
                                   };
                yield return userRole;
            }
        }

        /// <summary>
        /// Create an ad-hoc Access Group for the <paramref name="target"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="target">The sak.</param>
        /// <param name="userIds">The person ids.</param>
        /// <returns></returns>
        public static int CreateAccessGroup(this IFunctionManager instance, Case target, params int[] userIds)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (target == null)
                throw new ArgumentNullException("target");

            if (userIds == null || userIds.Length == 0)
                throw new ArgumentException(
                    Resources
                        .FunctionManagerExtensions_CreateAccessGroup_The_must_be_at_least_one_member_in_the_Access_Group,
                    "userIds");

            return
                (int)
                instance.Execute("OpprettTilgangsgruppeHandler", target.Id, 0,
                                 string.Join(",",
                                             userIds.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray()));
        }

        /// <summary>
        /// Create an Ad-Hoc Access Group for the <paramref name="target"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="target">The target registry entry.</param>
        /// <param name="userIds">The user ids.</param>
        /// <returns></returns>
        public static int CreateAccessGroup(this IFunctionManager instance, RegistryEntry target, params int[] userIds)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (target == null)
                throw new ArgumentNullException("target");

            if (userIds == null || userIds.Length == 0)
                throw new ArgumentException(
                    Resources
                        .FunctionManagerExtensions_CreateAccessGroup_The_must_be_at_least_one_member_in_the_Access_Group,
                    "userIds");

            return
                (int)
                instance.Execute("OpprettTilgangsgruppeHandler", 0, target.Id,
                                 string.Join(",",
                                             userIds.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray()));
        }

        /// <summary>
        /// Creates the reply.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="target">The target.</param>
        /// <param name="replyFromRecipientId">The reply from recipient id.</param>
        /// <param name="accessGroupId">The access group id.</param>
        /// <param name="downgradingCodeId">The downgrading code id.</param>
        /// <param name="downgradingDate">The downgrading date.</param>
        /// <param name="documentCategory">The document category.</param>
        /// <returns>
        /// The Id of the registry entry representing the reply.
        /// </returns>
        public static int CreateReply(this IFunctionManager instance, RegistryEntry target, int replyFromRecipientId = 0,
                                      int accessGroupId = -1, string downgradingCodeId = "", string downgradingDate = "",
                                      string documentCategory = "")
        {
            return (int) instance.Execute("BesvarJournalpostMedSkjerming",
                                          target.Id,
                                          replyFromRecipientId,
                                          target.RegistryEntryTypeId == "N" ? "X" : "U",
                                          target.IsPhysical.HasValue && target.IsPhysical.Value ? "BU" : "***",
                                          target.Title,
                                          target.TitleRestricted,
                                          target.TitlePersonNameTagged,
                                          target.AccessCodeId,
                                          accessGroupId,
                                          target.Pursuant,
                                          downgradingCodeId,
                                          downgradingDate,
                                          documentCategory);
        }

        /// <summary>
        /// Creates the case party letter.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="target">The target.</param>
        /// <param name="type">The type.</param>
        /// <param name="includeCaseParties">if set to <c>true</c> [include case parties].</param>
        /// <returns></returns>
        public static int CreateCasePartyLetter(this IFunctionManager instance, RegistryEntry target, string type,
                                                bool includeCaseParties)
        {
            return (int) instance.Execute("NyttPartsbrev", target.Id, type, includeCaseParties);
        }

        /// <summary>
        /// Marks the registry entry document as complete.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        public static void MarkRegistryEntryDocumentAsComplete(this IFunctionManager instance, int registryEntryId,
                                                               int documentDescriptionId)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            instance.Execute("FerdigstillDokument", registryEntryId, documentDescriptionId);
        }

        /// <summary>
        /// Marks the <see cref="RegistryEntryDocument"/> as complete.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryDocument">The target.</param>
        public static void MarkRegistryEntryDocumentAsComplete(this IFunctionManager instance,
                                                               RegistryEntryDocument registryEntryDocument)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (registryEntryDocument == null)
                throw new ArgumentNullException("registryEntryDocument");

            instance.MarkRegistryEntryDocumentAsComplete(registryEntryDocument.RegistryEntryId,
                                                         registryEntryDocument.DocumentDescriptionId);
        }

        /// <summary>
        /// Requests the document approval.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="documentDescriptionId">The document description id.</param>
        /// <param name="approverUserNameId">The approver user name id.</param>
        public static void RequestDocumentApproval(this IFunctionManager instance, int registryEntryId,
                                                   int documentDescriptionId, int? approverUserNameId = null)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            instance.Execute("GodkjennDokument", registryEntryId, documentDescriptionId,
                             (approverUserNameId ?? 0).ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Requests the document approval.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryDocument">The registry entry document.</param>
        /// <param name="approverUserNameId">The approver user name id.</param>
        public static void RequestDocumentApproval(this IFunctionManager instance,
                                                   RegistryEntryDocument registryEntryDocument,
                                                   int? approverUserNameId = null)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (registryEntryDocument == null)
                throw new ArgumentNullException("registryEntryDocument");

            instance.RequestDocumentApproval(registryEntryDocument.RegistryEntryId,
                                             registryEntryDocument.DocumentDescriptionId, approverUserNameId ?? 0);
        }

        /// <summary>
        /// Approves the registry entry.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="caseId">The case id.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <param name="remark">The remark.</param>
        /// <param name="notifyByEmail">if set to <c>true</c> [notify by email].</param>
        public static void MarkRegistryEntryAsApproved(this IFunctionManager instance, int caseId, int registryEntryId,
                                                       bool isApproved, string remark, bool notifyByEmail)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            instance.Execute("GodkjennJournalpost", caseId, registryEntryId, isApproved, remark, notifyByEmail);
        }

        /// <summary>
        /// Approves the registry entry.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntry">The registry entry.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <param name="remark">The remark.</param>
        /// <param name="notifyByEmail">if set to <c>true</c> [notify by email].</param>
        public static void MarkRegistryEntryAsApproved(this IFunctionManager instance, RegistryEntry registryEntry,
                                                       bool isApproved, string remark, bool notifyByEmail)
        {
            if (registryEntry == null)
                throw new ArgumentNullException("registryEntry");

            if (!registryEntry.CaseId.HasValue)
                throw new ArgumentException(
                    Resources.FunctionManagerExtensions_ApproveRegistryEntry_The_registry_entry_must_have_a_valid_CaseId,
                    "registryEntry");

            MarkRegistryEntryAsApproved(instance, registryEntry.CaseId.Value, registryEntry.Id, isApproved, remark,
                                        notifyByEmail);
        }

        /// <summary>
        /// Marks the registry entry as read.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        public static void MarkRegistryEntryAsRead(this IFunctionManager instance, int? registryEntryId)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (!registryEntryId.HasValue)
                throw new ArgumentNullException("registryEntryId");

            instance.Execute("MarkerJournalpostSomLest", registryEntryId);
        }

        /// <summary>
        /// Marks the registry entry as read.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntry">The registry entry.</param>
        public static void MarkRegistryEntryAsRead(this IFunctionManager instance, RegistryEntry registryEntry)
        {
            if (registryEntry == null)
                throw new ArgumentNullException("registryEntry");

            MarkRegistryEntryAsRead(instance, registryEntry.Id);
        }

        /// <summary>
        /// Marks the case as read.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="caseId">The case id.</param>
        public static void MarkCaseAsRead(this IFunctionManager instance, int caseId)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            instance.Execute("MarkerSakSomLest", caseId);
        }

        /// <summary>
        /// Marks the case as read.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="readCase">The read case.</param>
        public static void MarkCaseAsRead(this IFunctionManager instance, Case readCase)
        {
            if (readCase == null)
                throw new ArgumentNullException("readCase");

            MarkCaseAsRead(instance, readCase.Id);
        }

        /// <summary>
        /// Marks the case as complete.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="caseId">The case id.</param>
        public static void MarkCaseAsComplete(this IFunctionManager instance, int caseId)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            instance.Execute("SettSakTilFerdigmeldt", caseId);
        }

        /// <summary>
        /// Marks the case as complete.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="completedCase">The @case.</param>
        public static void MarkCaseAsComplete(this IFunctionManager instance, Case completedCase)
        {
            if (completedCase == null)
                throw new ArgumentNullException("completedCase");

            MarkCaseAsComplete(instance, completedCase.Id);
        }

        /// <summary>
        /// Marks the registry entry as followed up.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="recipientId">The recipient id.</param>
        /// <param name="followUpMethod">The follow up method.</param>
        /// <param name="remark">The remark.</param>
        public static void MarkRegistryEntryAsFollowedUp(this IFunctionManager instance, int registryEntryId,
                                                         int recipientId, string followUpMethod, string remark)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            instance.Execute("AvskrivJournalpost", registryEntryId, recipientId, followUpMethod, remark);
        }

        /// <summary>
        /// Marks the registry entry as followed up.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="recipient">The recipient.</param>
        /// <param name="followUpMethod">The follow up method.</param>
        /// <param name="remark">The remark.</param>
        public static void MarkRegistryEntryAsFollowedUp(this IFunctionManager instance, SenderRecipient recipient,
                                                         string followUpMethod, string remark)
        {
            if (recipient == null)
                throw new ArgumentNullException("recipient");

            if (!recipient.RegistryEntryId.HasValue)
                throw new ArgumentException(
                    Resources.FunctionManagerExtensions_The_recipient_must_have_a_valid_RegistryEntryId, "recipient");

            MarkRegistryEntryAsFollowedUp(instance, recipient.RegistryEntryId.Value, recipient.Id, followUpMethod,
                                          remark);
        }

        /// <summary>
        /// Marks the sender recipient as read.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="senderRecipientId">The sender recipient id.</param>
        public static void MarkSenderRecipientAsRead(this IFunctionManager instance, int senderRecipientId)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            instance.Execute("MarkerAvsenderMottakerSomLest", senderRecipientId);
        }

        /// <summary>
        /// Marks the sender recipient as read.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="senderRecipient">The sender recipient.</param>
        public static void MarkSenderRecipientAsRead(this IFunctionManager instance, SenderRecipient senderRecipient)
        {
            if (senderRecipient == null)
                throw new ArgumentNullException("senderRecipient");

            MarkSenderRecipientAsRead(instance, senderRecipient.Id);
        }

        /// <summary>
        /// Marks the task as complete.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="taskId">The task id.</param>
        public static void MarkTaskAsComplete(this IFunctionManager instance, int taskId)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            instance.Execute("MerkAktivitetSomFullfoert", taskId);
        }

        /// <summary>
        /// Marks the task as complete.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="task">The task.</param>
        public static void MarkTaskAsComplete(this IFunctionManager instance, Task task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            MarkTaskAsComplete(instance, task.Id);
        }

        /// <summary>
        /// Designates the registry entry as reply.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="replyToSenderId">The reply to sender id.</param>
        /// <param name="replyToRegistryEntryId">The reply to registry entry id.</param>
        /// <param name="replyWithRegistryEntryId">The reply with registry entry id.</param>
        /// <param name="followUpMethod">The follow up method.</param>
        /// <param name="documentCategory">The document category.</param>
        public static void DesignateRegistryEntryAsReply(this IFunctionManager instance, int replyToSenderId,
                                                         int replyToRegistryEntryId, int replyWithRegistryEntryId,
                                                         string followUpMethod, string documentCategory)
        {
            instance.Execute("BesvarInngaaendeJournalposterMedUtgaaende", new object[]
                                                                              {
                                                                                  replyToSenderId,
                                                                                  replyWithRegistryEntryId,
                                                                                  followUpMethod,
                                                                                  replyToRegistryEntryId.ToString(
                                                                                      CultureInfo.InvariantCulture),
                                                                                  documentCategory
                                                                              });

        }

        /// <summary>
        /// Distributes the registry entry.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="registryEntryId">The registry entry id.</param>
        /// <param name="administrativeUnitId">The administrative unit id.</param>
        /// <param name="userNameId">The user name id.</param>
        /// <param name="registryManagementUnitId">The registry management unit id.</param>
        /// <param name="sendEmail">if set to <c>true</c> [send email].</param>
        /// <param name="remark">The remark.</param>
        /// <param name="newRecipients">The new recipients.</param>
        public static void DistributeRegistryEntry(this IFunctionManager instance, int registryEntryId,
                                                   int administrativeUnitId, int userNameId,
                                                   string registryManagementUnitId, bool sendEmail, string remark,
                                                   IEnumerable<SenderRecipient> newRecipients)
        {
            var newRecipientSerialized = new StringBuilder();
            using (var nyeMottakereWriter = XmlWriter.Create(newRecipientSerialized))
            {
                var serializer = new DataContractSerializer(typeof (SenderRecipient[]), new[] {typeof (SenderRecipient)});
                serializer.WriteObject(nyeMottakereWriter, newRecipients.ToArray());
            }

            instance.Execute("DistributeV3En", registryEntryId, administrativeUnitId, userNameId,
                             registryManagementUnitId, sendEmail, remark, newRecipientSerialized.ToString());
        }
    }
}