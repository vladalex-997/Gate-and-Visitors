<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StatusInstruire.aspx.cs" Inherits="proiectPaza.StatusInstruire" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <link rel="stylesheet" type="text/css" href="StyleSheetInregistrari.css" />

    <section id="main-content">

        <header class="panel-heading">

             <div class="col-md-8 col-md-offset-4">

                <h1> Status/Instruire Persoane </h1>

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

             <asp:Label Text="* Nume si Prenume:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxNume" CssClass="form-control input-sm" placeholder="Nume si Prenume" />


                </div>



                </div>

                <div class="col-md-2 col-md-offset-0">

                    <div class="form-group">

             <asp:Label Text="* Serie Buletin:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxSerieBuletin" CssClass="form-control input-sm" placeholder="Serie Buletin" />


                </div>



                </div>

               <%--         <div class="col-md-2 col-md-offset-0">

                    <div class="form-group">

             <asp:Label Text="* Companie" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxCompanie" CssClass="form-control input-sm" placeholder="Companie" />


                </div>



                </div>--%>

                     <div class="col-md-2 col-md-offset-0">

                    <div class="form-group">

             <asp:Label Text="* Timp Instruire valabilitate (zile)" runat="server" />
             
                         <asp:DropDownList ID="DropDownListTimpInstruire" runat="server" Width="100px" Height="30px">  
                        </asp:DropDownList> 

                </div>



                </div>

                <div class="col-md-2 col-md-offset-0">

                    <div class="form-group">

                        <div style="margin-top:15px">

                         <asp:Button Text="Instruire" ID="btnInstruire" CssClass="btn btn-primary" Width="150px" Height="40px" runat="server" OnClick="btnInstruire_Click"/>
                    
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

                   

                     <asp:Button Text="Refresh" ID="btnRefresh" CssClass="btn btn-primary" Width="150px" Height="40px" runat="server" OnClick="btnRefresh_Click" />
                    
                        
                </div>

                </div>


                 <div class="col-md-2 col-md-offset-0">

                <div class="form-group">

                     <asp:Button Text="Toate Inregistrarile" ID="btnToate" CssClass="btn btn-primary" Width="150px" Height="40px" runat="server" OnClick="btnToate_Click" />
                    

                </div>

                </div>


           


               

            </div>

            </br>
            </br>



            <div class="row">

                <div class="col-md-4 col-md-offset-4">

                    <div class="form-group">

                        <h4> Inregistrarile Prezente: </h4>


                    </div>

                </div>

            </div>

               

            <div class="row">

                <div class="col-md-10 col-md-offset-0">

                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-bordered table-striped" Width="1200px" OnRowDataBound="GridView1_RowDataBound" >

            <Columns>
                <asp:BoundField DataField="IdRecord" HeaderText="Numar Programare" />
                <asp:BoundField DataField="Nume" HeaderText="Nume si Prenume" />
                <asp:BoundField DataField="Act" HeaderText="Detalii Act" />
               
                <asp:BoundField DataField="OraProgramata" HeaderText="Data/Ora Programarii" />
                 
                <asp:BoundField DataField="DataInstruire" HeaderText="Valabilitate Instruire pana la" />
                <asp:BoundField DataField="StatusInstruire" HeaderText="Status Instruire" />
                <asp:BoundField DataField="InstruitDe" HeaderText="Instruit De" />


                 <asp:BoundField DataField="Companie" HeaderText="Companie" />

                
               
              
                
            </Columns>

            </asp:GridView>


                </div>


              


            </div>


        </div>

    </section>


</asp:Content>
