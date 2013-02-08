<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="Gecko.NCore.Host._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<h2>
		Welcome to the service host!
	</h2>
	<p>
		This is a dummy host that exposes the metadata for the contracts. The services are
		not actually implemented.
	</p>
	<p>
		<ul>
		    <li>
				<h2>
					Change tracking service</h2>
				<ul>
					<li><a href="Services/ChangeTracking/V1/ChangeTrackingService.svc">V1</a></li>
				</ul>
			</li>
		    <li>
				<h2>
					Claims service</h2>
				<ul>
					<li><a href="Services/Claim/V1/ClaimService.svc">V1</a></li>
				</ul>
			</li>
		    <li>
				<h2>
					Document service</h2>
				<ul>
				    <li><a href="Services/Documents/V1/DocumentsService.svc">V1</a></li>
				    <li><a href="Services/Documents/V2/DocumentService.svc">V2</a></li>
					<li><a href="Services/Documents/V3/DocumentService.svc">V3</a></li>
				</ul>
			</li>
		    <li>
				<h2>
					Dynamic object model service</h2>
				<ul>
					<li><a href="Services/DynamicObjectModelService/V1/DynamicObjectModelService.svc">V1</a></li>
				</ul>
			</li>
		    <li>
				<h2>
					Ephorte web internal service</h2>
				<ul>
					<li><a href="Services/ePhorteWebInternal/ePhorteWebInternalService.svc">V1</a></li>
				</ul>
			</li>
			<li>
				<h2>
					Functions Service</h2>
				<ul>
				    <li><a href="Services/Functions/V1/FunctionsService.svc">V1</a></li>
					<li><a href="Services/Functions/V2/FunctionsService.svc">V2</a></li>
				</ul>
			</li>
            <li>
				<h2>
					Notification Service</h2>
				<ul>
					<li><a href="Services/Notification/V1/NotificationService.svc">V1</a></li>
				</ul>
			</li>
			<li>
				<h2>
					ObjectModel Service</h2>
				<ul>
				    <li><a href="Services/ObjectModel/V1/ObjectModelService.svc">V1</a></li>
                    <li><a href="Services/ObjectModel/V2/ObjectModelService.svc">V2</a></li>
					<li><a href="Services/ObjectModel/V3/En/ObjectModelService.svc">V3.En</a></li>
					<li><a href="Services/ObjectModel/V3/No/ObjectModelService.svc">V3.No</a></li>
				</ul>
			</li>
            <li>
                <h2>Metadata Service</h2>
                <ul>
                    <li><a href="Services/Metadata/V1/MetadataService.svc">V1</a></li>
                </ul>
            </li>
		</ul>
	</p>
</asp:Content>
