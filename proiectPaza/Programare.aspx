<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Programare.aspx.cs" Inherits="proiectPaza.Programare" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

       

    <section id="main-content">

        <header class="panel-heading">

             <div class="col-md-4 col-md-offset-4">
                           <h1>
                               Programari Vizite

                           </h1>


                       </div>

        </header>

    </br>
    </br>
    </br>
   
        

        <div class="container-fluid">

            <div class="row">
                <div class="col-md-12 col-md-offset-0">

                     <h4>Toate campurile cu * sunt OBLIGATORII</h4>

                </div>
               

            </div>

            </br>
            </br>

            <div class="row">

                <div class="col-md-4 col-md-offset-0">
      <div class="form-group">

             <asp:Label Text="* Nume:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxNume" CssClass="form-control input-sm" placeholder="Nume" />
                                        
       </div>
    </div>
    <div class="col-md-4 col-md-offset-0">
      <div class="form-group">

             <asp:Label Text="* Act Serie/Numar:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxAct" CssClass="form-control input-sm" placeholder="Act Serie/Numar" />

        </div>
    </div>

                     <div class="col-md-4 col-md-offset-0">

        <div class="form-group">

            <asp:Label Text="* Destinatie:" runat="server" />
           </br>
            
            <asp:DropDownList ID="DropDownListdestinatie" runat="server" Width="300px" Height="30px">  
        </asp:DropDownList> 


        </div>

        </div>

            </div>

            </br>
            

    <div class="row">

   

        <div class="col-md-4 col-md-offset-0">

        <div class="form-group">

            <asp:Label Text="* Motiv vizita:" runat="server" />
            </br>
           
            <asp:DropDownList ID="DropDownListmotiv" runat="server" Width="300px" Height="30px">
                
        </asp:DropDownList> 


        </div>

        </div>


        <div class="col-md-4 col-md-offset-0">

            <div class="form-group">

                <asp:Label Text="* Data/Ora programare vizita:" runat="server" />
              <asp:TextBox runat="server" TextMode="DateTimeLocal" Enabled="true" ID="tbxOraProgramare" CssClass="form-control input-sm" />
             

            </div>

        </div>

         <div class="col-md-4 col-md-offset-0">

            <div class="form-group">

                <asp:Label Text="* Data Expirare:" runat="server" />
              <asp:TextBox runat="server" TextMode="DateTimeLocal" Enabled="true" ID="tbxDataExpirare" CssClass="form-control input-sm" />
             

            </div>

        </div>

    </div>
            </br>

            <div class="row">

                <div class="col-md-4 col-md-offset-0">
      <div class="form-group">

             <asp:Label Text="Email (necesar pentru Programare si Confirmare):" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxEmail" CssClass="form-control input-sm" placeholder="Email" />

        </div>
    </div>



            </div>

            </br>
            </br>

    

          

            <div class="row">
                <div class="col-md-12 col-md-offset-0">

                     <h4>Sectiune Companii</h4>

                </div>
               

            </div>

            </br>
           


            <div class="row">

                <div class="col-md-4 col-md-offset-0">
      <div class="form-group">

             <asp:Label Text="Companie:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxCompanie" CssClass="form-control input-sm" placeholder="Companie" />

        </div>
    </div>


                <div class="col-md-4 col-md-offset-0">
      <div class="form-group">

             <asp:Label Text="Numar masina:" runat="server" />
             <asp:TextBox runat="server" Enabled="true" ID="tbxNumarMasina" CssClass="form-control input-sm" placeholder="Numar masina" />

        </div>
    </div>



                




            </div>  

         

            <div class="row">

                <div class="col-md-8 col-md-offset-0">

                    <h5>Butonul Programare si Confirmare realizeaza si o confirmare pe Email.</h5>
                    <h5>Dupa ce apasati asteptati confirmarea Paginii (dureaza maxim 30 de secunde).</h5>
                 

                     

                </div>

                </div>
            </br>
                

                <div class="row">

                    <div class="col-md-4 col-md-offset-0">

                        <asp:Button Text="Programare" ID="btnProgramare" CssClass="btn btn-primary" Width="200px" Height="40px" runat="server" OnClick="btnProgramare_Click" />
                    

                    </div>

                    <div class="col-md-4 col-md-offset-0">

                    <asp:Button Text="Programare si Confirmare" ID="btnProgramareConfirm" CssClass="btn btn-primary" Width="200px" Height="40px" runat="server" OnClick="btnProgramareConfirm_Click" />


                </div>

                    <div class="col-md-4 col-md-offset-0">

                  <%--  <asp:Button Text="Test" ID="Button1" CssClass="btn btn-primary" Width="200px" Height="40px" runat="server" OnClick="Button1_Click" />--%>


                </div>

                </div>

                

           


        </div>

   


    </section>

</asp:Content>
