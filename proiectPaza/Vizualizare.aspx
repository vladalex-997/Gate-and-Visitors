<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vizualizare.aspx.cs" Inherits="proiectPaza.Vizualizare" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" type="text/css" href="StyleSheetInregistrari.css" />


    <section id="main-content">

        <header class="panel-heading">

            <div class="col-md-8 col-md-offset-4">

                <h1> Vizualizare Inregistrari </h1>

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

                <div class="col-md-2 col-md-offset-0">

                <div class="form-group">

             <asp:Label Text="Nume (Filtrare):" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxNume" CssClass="form-control input-sm" placeholder="Nume si Prenume" />


                </div>



            </div>

                   <div class="col-md-2 col-md-offset-0">

                <div class="form-group">

             <asp:Label Text="Status:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxStatus" CssClass="form-control input-sm" placeholder="Status" />

                </div>



            </div>

                   <div class="col-md-2 col-md-offset-2">

                <div class="form-group">

                    <asp:Label Text="* Numar Programare:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxNumarProgramare" CssClass="form-control input-sm" placeholder="Numar Programare" />

                </div>



            </div>

                   <div class="col-md-2 col-md-offset-0">

                <div class="form-group">

                     <asp:Label Text="Cartela atribuita:   " runat="server" />
           <%-- </br>
           
            <asp:DropDownList ID="DropDownListcartela" runat="server" Width="140px" Height="30px">

                </asp:DropDownList> --%>
                </br>
                

                     <asp:Label Text="    N/A  " ID="atribuirecartela" runat="server" />

                </div>
                    



            </div>

                 <div class="col-md-2 col-md-offset-0">

                     <div class="form-group">

                         <div style="margin-top:15px">

                              <asp:Button Text="Intrare" ID="btnIntrare" CssClass="btn btn-primary" Width="100px" Height="40px" BackColor="Green" runat="server" OnClick="btnIntrare_Click" />
                    

                         </div>

                        

                     </div>



                 </div>


            </div>

            </br>
            </br>
            </br>
            


            <div class="row">

                 <div class="col-md-2 col-md-offset-0">

                     <div class="form-group">

                          <asp:Button Text="Filtrare" ID="btnFiltrare" CssClass="btn btn-primary" Width="100px" Height="40px" runat="server" OnClick="btnFiltrare_Click" />
                    

                     </div>



                 </div>

                <div class="col-md-2 col-md-offset-0">

                     <div class="form-group">

                         <asp:Button Text="Toate Inregistrarile" ID="btnToate" CssClass="btn btn-primary" Width="150px" Height="40px" runat="server" OnClick="btnToate_Click" />
                    

                     </div>



                 </div>

                       <div class="col-md-2 col-md-offset-2">

                <div class="form-group">

             <asp:Label Text="* Numar Iesire:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxNumarIesire" CssClass="form-control input-sm" placeholder="Numar Iesire" />


                </div>



            </div>

                        <div class="col-md-2 col-md-offset-0">

                <div class="form-group">

             <asp:Label Text="* Numar Cartela Iesire:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxCartelaIesire" CssClass="form-control input-sm" Width="140px" placeholder="Numar Cartela (Iesire)" />


                </div>



            </div>

               


                <div class="col-md-2 col-md-offset-0">

                     <div class="form-group">

                         <div style="margin-top:15px">

                             <asp:Button Text="Iesire" ID="btnIesire" CssClass="btn btn-primary" Width="100px" Height="40px" BackColor="Red" runat="server" OnClick="btnIesire_Click" />
                    

                         </div>

                         

                     </div>



                 </div>



            </div>

            </br>
            </br>

            <div class="row">

                <div class="col-md-4 col-md-offset-0">

                    <div class="form-group">

                         <asp:Label Text="Id Neprezentat:" runat="server" />
                         <asp:TextBox runat="server" Enabled="true" ID="tbxNeprezentare" CssClass="form-control input-sm" placeholder="Id Neprezentat" Width="150px" />
                        </br>

                         <asp:Button Text="Inchide prin Neprezentare" ID="btnNeprezentare" CssClass="btn btn-primary" Width="180px" Height="40px" BackColor="#cccc00" runat="server" OnClick="btnNeprezentare_Click" />
                    



                    </div>

                </div>

            </div>

               </br>
            </br>

            <div class="row">

                <div class="col-md-4 col-md-offset-4">

                    <div class="form-group">

                        <h4>Programarile / Inregistrarile Prezente: </h4>


                    </div>

                </div>

            </div>


            </br>
            </br>


            <div class="row">

                <div class="col-md-10 col-md-offset-0">

                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-bordered table-striped" Width="1200px" OnRowDataBound="GridView1_RowDataBound" >

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

                 <asp:BoundField DataField="ProgramatDe" HeaderText="Programat De" />

                  <asp:BoundField DataField="StatusInstruire" HeaderText="Status Instruire" />
               
              
                
            </Columns>

            </asp:GridView>


                </div>


              


            </div>
            



        </div>


    </section>

</asp:Content>
