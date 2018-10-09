<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="navbar.ascx.cs" Inherits="GTICLOUD.navbar" %>


<ul id="dropdown1" class="dropdown-content">

    <asp:Label ID="dropdown" runat="server" Text="Label"></asp:Label>
</ul>
<nav class="theme-bg-color"> 

        <ul class="right hide-on-med-and-down">
    <!--  <li><a href="sass.html">Sass</a></li>
      <li><a href="badges.html">Components</a></li>--->
      <!-- Dropdown Trigger -->
      <li><a class="dropdown-trigger" href="#!" data-target="dropdown1">Account<i class="material-icons right">arrow_drop_down</i></a></li>
    </ul>
</nav>