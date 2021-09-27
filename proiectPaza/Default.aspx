<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proiectPaza._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section id="wrapper">

        <div class="row">

            <div class="col-lg-12">

                <section class="panel">

                    <header class="panel-heading">

                         <div class="col-md-8 col-md-offset-3">

                                <h1>Autentificare Aplicatie Vizitatori</h1>


                            </div>

                    </header>

                         </br>
                         </br>
                         </br>
                         </br>
                         </br>
                         </br>

                 <div class="panel-body">

                           


                            <div class="row">

                                <div class="çol-md-4 col-md-offset-4">

                                    <div class="form-group">

                                        <asp:Label Text="* Username din domain :" runat="server" />
                                        <asp:TextBox runat="server" Enabled="true" ID="tbxEmail" CssClass="form-control input-sm" placeholder="Username" />

                                    </div>

                                </div>

                                <div class="çol-md-4 col-md-offset-4">

                                    <div class="form-group">

                                        <asp:Label Text="* Parola din domain :" runat="server" />
                                        <asp:TextBox runat="server" Enabled="true" ID="tbxParola" TextMode="Password" CssClass="form-control input-sm" placeholder="Parola" />
                                        </br>
                                        <h5>Toate campurile cu * sunt OBLIGATORII in aceasta aplicatie</h5>
                                        </br>
                                        </br>
                                        
                                        <asp:Button Text="Log-In" ID="btnLogin" CssClass="btn btn-primary" Width="150px" runat="server" OnClick="btnLogin_Click" />
                                        </br>
                                        </br>
                                        <asp:Label Text="" runat="server" ID="statuslogin" />
                                    </div>

                                </div>

                                <div class="row">

                                    <div class="çol-md-4 col-md-offset-4">

                                        
                                        
                                    </div>


                                </div>


                            </div>


                        </div>


                </section>

            </div>
        </div>

    </section>
    

</asp:Content>
