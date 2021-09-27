<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Instruire.aspx.cs" Inherits="proiectPaza.Instruire" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <link rel="stylesheet" type="text/css" href="StyleSheetInregistrari.css" />


    <section id="main-content">

        <header class="panel-heading">

             <div class="col-md-8 col-md-offset-4">

                <h1> Sectiune Instruire </h1>

            </div>

        </header>

        </br>
        </br>
        </br>
        </br>
        </br>
        </br>

        <div class="container-fluid">

            <div class="row">

                <div class="col-md-4 col-md-offset-4">

                    <div class="form-group">

             <asp:Label Text="* Serie Buletin:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxSerieBuletin" CssClass="form-control input-sm" placeholder="Serie Buletin" />


                </div>



                </div>


            </div>
            </br>

             <div class="row">

                <div class="col-md-4 col-md-offset-4">

                    <div class="form-group">

             <asp:Label Text="* Nume si Prenume:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxNume" CssClass="form-control input-sm" placeholder="Nume si Prenume" />


                </div>



                </div>


            </div>

            </br>

             <div class="row">

                <div class="col-md-4 col-md-offset-4">

                    <div class="form-group">

             <asp:Label Text="* Companie" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxCompanie" CssClass="form-control input-sm" placeholder="Companie" />


                </div>



                </div>


            </div>

            </br>

                 <div class="row">

                <div class="col-md-4 col-md-offset-4">

                    <div class="form-group">

             <asp:Label Text="* Timp Instruire valabilitate (zile)" runat="server" />
             </br>
                         <asp:DropDownList ID="DropDownListTimpInstruire" runat="server" Width="100px" Height="30px">  
                        </asp:DropDownList> 

                </div>



                </div>


            </div>

            </br>
            </br>

            <div class="row">

                <div class="col-md-4 col-md-offset-4">

                     <asp:Button Text="Instruire" ID="btnInstruire" CssClass="btn btn-primary" Width="200px" Height="40px" runat="server" OnClick="btnInstruire_Click" />
                    

                </div>

            </div>

        </div>


    </section>



</asp:Content>
