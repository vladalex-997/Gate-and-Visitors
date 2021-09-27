<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProgramareCont.aspx.cs" Inherits="proiectPaza.ProgramareCont" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <link rel="stylesheet" type="text/css" href="StyleSheetInregistrari.css" />

    <section id="main-content">

        <header class="panel-heading">

             <div class="col-md-12 col-md-offset-3">

                <h2> Vizualizare Programari realizate de contul curent </h2>

            </div>

        </header>


        </br>
        </br>
        </br>
        </br>
        </br>
        </br>
        </br>

        <div class="container-fluid">

            <div class="row">


                  <div class="col-md-4 col-md-offset-0">

                     <div class="form-group">

             <asp:Label Text="Filtrare dupa Nume si Prenume Programat:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxNumeProgramat" CssClass="form-control input-sm" placeholder="Nume Programat" />

                </div>

                </div>

                <div class="col-md-3 col-md-offset-0">

                    <div class="form-group">

                         <div style="margin-top:15px">

                              <asp:Button Text="Filtrare" ID="btnFiltrare" CssClass="btn btn-primary" Width="100px" Height="40px" runat="server" OnClick="btnFiltrare_Click" />
                    

                         </div>

                        

                     </div>

                </div>

            </div>

            </br>
            </br>

            <div class="row">

                
                <div class="col-md-10 col-md-offset-0">

                    <h4>Programarile prezente in baza de date pentru contul curent:</h4>

                </div>


            </div>

            </br>
            </br>

            <div class="row">


                <div class="col-md-10 col-md-offset-0">

                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-bordered table-striped" Width="1200px" >

            <Columns>
                <asp:BoundField DataField="IdRecord" HeaderText="Numar Programare" />
                <asp:BoundField DataField="Nume" HeaderText="Nume si Prenume" />
                <asp:BoundField DataField="Act" HeaderText="Detalii Act" />
               
                <asp:BoundField DataField="OraProgramata" HeaderText="Data/Ora Programarii" />
                 <asp:BoundField DataField="DataExpirare" HeaderText="Data Expirare Acces" />
                <asp:BoundField DataField="StatusPersoana" HeaderText="Status Persoana" />

                <asp:BoundField DataField="Cartela" HeaderText="Numar Cartela" />

               
                 <asp:BoundField DataField="NumarMasina" HeaderText="Auto" />
                 <asp:BoundField DataField="Companie" HeaderText="Companie" />

                
              
                
            </Columns>

            </asp:GridView>

                </div>


            </div>

        </div>

    </section>


</asp:Content>
