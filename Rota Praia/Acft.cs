using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace rota_praia1
{
    public class Acft
    {

        #region ----------------atributos privados-------------------

            private string matricula { get; set; }
            private double velocidade { get; set; }
            private int altitude { get; set; }
            private double tamFonte { get; set; }
            private Local posicao = new Local();

            private Color corLinhas;
            private Color corPerimetro;
            private bool mostrarMatricula = true;
            private bool mostrarRumo = true;
            private bool mostrarPerimetroLateral = true;

        #endregion ----------------final atributos privados-------------------

        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        #region ----------------atributos publicos-------------------

            public BO_ACFTs listaACFTs_parent;

            //
            public int Id;
            public Grupo donoGrupo;
            //

            public PictureBox picAcft = new PictureBox();
            public Label lblMatricula = new Label();
            public Label lblAltitude = new Label();

            public bool selecionado = false;

            #region Velocidade
            
                public double espaco;
                public double tempo;
                public int Incremento = 1; //Int32.Parse(Math.Round(acft.velocidade / 100).ToString())
        
            #endregion

            #region Movimento

                public Timer timer = new Timer();
                public List<Local> locais = new List<Local>();
                public List<Reta> retas = new List<Reta>();
                public int indiceRetaAtual = 0;
                public bool descer, direita;

            #endregion

            #region Verificacao de Alertas
                
                public int distanciaLateralMin = 50;
                public int distanciaVerticalMin = 100;

                public bool piscarFundo = false;
                public bool piscarFundoAlerta = false;
                
            #endregion

        #endregion ----------------final atributos publicos-------------------


        public void Inicializar()
        {
            posicao.Inicializar();

            picAcft.Dispose();
            lblMatricula.Dispose();
            lblAltitude.Dispose();

            timer.Dispose();
            locais.ForEach(o => o.Inicializar());
            retas.ForEach(o => o.Inicializar());
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        #region ----------------propriedades publicas-------------------

            [Display(Name = "Matrícula")]
            public string Matricula
            {
                get
                {
                    return matricula;
                }
                set
                {
                    matricula = value;
                    lblMatricula.Text = value;
                }
            }

            [Display(Name = "Tipo ACFT")]
            public string Tipo { get; set; }

            [Display(Name = "Velocidade")]
            public double Velocidade
            {
                get
                {
                    return velocidade;
                }
                set
                {
                    if (value != 0)
                    {
                        velocidade = value;
                        timer.Interval = this.GerarTimer(value);
                    }
                }
            }

            [Display(Name = "Altitude")]
            public int Altitude
            {
                get
                {
                    return altitude;
                }
                set
                {
                    altitude = value;
                    lblAltitude.Text = value.ToString();
                }
            }

            public Color CorFonteMatricula
            {
                get
                {
                    return lblMatricula.ForeColor;
                }
                set
                {
                    lblMatricula.ForeColor = value;
                }
            }

            public Color CorFonteAltitude
            {
                get
                {
                    return lblAltitude.ForeColor;
                }
                set
                {
                    lblAltitude.ForeColor = value;
                }
            }
            
            public Color CorLinhas
            {
                get
                {
                    return corLinhas;
                }
                set
                {
                    corLinhas= value;
                }
            }
            
            public Color CorPerimetro
            {
                get
                {
                    return corPerimetro;
                }
                set
                {
                    corPerimetro = value;
                }
            }
            
            [Display(Name = "Eixo X")]
            public int X
            {
                get
                {
                    return lblMatricula.Left;
                }
                set
                {
                    lblMatricula.Left = value;
                }
            }

            [Display(Name = "Eixo Y")]
            public int Y
            {
                get
                {
                    return lblMatricula.Top;
                }
                set
                {
                    lblMatricula.Top = value;
                }
            }

            [Display(Name = "Posição")]
            public Local Posicao
            {
                get
                {
                    posicao.X = this.X;
                    posicao.Y = this.Y;

                    return posicao;
                }
            }
            
            [Display(Name = "Mostrar Matrícula")]
            public bool MostrarMatricula
            {
                get
                {
                    return mostrarMatricula;
                }
                set
                {
                    mostrarMatricula = value;
                    if (value)
                        lblMatricula.Text = matricula;
                    else
                        lblMatricula.Text = Tipo;
                }
            }

            [Display(Name = "Mostrar Rumo")]
            public bool MostrarRumo
            {
                get
                {
                    return mostrarRumo;
                }
                set
                {
                    mostrarRumo = value;
                }
            }

            [Display(Name = "Mostrar Perímetro")]
            public bool MostrarPerimetro
            {
                get
                {
                    return mostrarPerimetroLateral;
                }
                set
                {
                    mostrarPerimetroLateral = value;
                }
            }

        #endregion ----------------final propriedades publicas-------------------

        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        #region ----------------Métodos privados---------------------

            private int GerarTimer(double p)
            {
                int velocidade = Int32.Parse(Math.Round(60000 / p).ToString());
                return velocidade;
            }
            
            private void timer_Tick(object sender, EventArgs e)
            {
                //int i = Int32.Parse((sender as Timer).Tag.ToString());

                Reta retaAtual = retas[indiceRetaAtual];

                if (Math.Abs(retaAtual.Fim.X - retaAtual.Ini.X) > Math.Abs(retaAtual.Fim.Y - retaAtual.Ini.Y))
                {
                    if (direita)
                        this.X = this.X + this.Incremento; //Int32.Parse(Math.Round(acft.velocidade / 100).ToString())
                    else
                        this.X = this.X - this.Incremento; //Int32.Parse(Math.Round(acft.velocidade / 100).ToString())

                    if (retaAtual.b != 0)
                        this.Y = -(retaAtual.a * this.X + retaAtual.c) / retaAtual.b;
                    //else
                    //    label.BackColor = Color.Red;
                }
                else
                {
                    if (descer)
                        this.Y = this.Y + this.Incremento; //Int32.Parse(Math.Round(acft.velocidade / 100).ToString())
                    else
                        this.Y = this.Y - this.Incremento; //Int32.Parse(Math.Round(acft.velocidade / 100).ToString())

                    if (retaAtual.b != 0)
                        this.X = -(retaAtual.b * this.Y + retaAtual.c) / retaAtual.a;
                    //else
                    //    label.BackColor = Color.Yellow;
                }

                if (piscarFundo)
                {
                    if (lblMatricula.BackColor == Color.Gray)
                        lblMatricula.BackColor = Color.White;
                    else
                        lblMatricula.BackColor = Color.Gray;
                }
                else
                    lblMatricula.BackColor = Color.White;

                if (piscarFundoAlerta)
                {
                    if (lblMatricula.BackColor == Color.Yellow)
                        lblMatricula.BackColor = Color.White;
                    else
                        lblMatricula.BackColor = Color.Yellow;
                }

                //preserva a cor de fundo da acft selecionada
                if (selecionado && lblMatricula.BackColor == Color.White)
                {
                    lblMatricula.BackColor = Color.Cyan;
                }

                this.VerificarTrocarRumo();

                this.AtualizarDadosAltitude();

                this.listaACFTs_parent.imgCarta.Refresh();

                //this.VerificarDistanciaEntreACFTs(10, 200);

                //distancia = Math.Sqrt(Math.Abs(acft.xFin - acft.xIni) + Math.Abs(acft.yFin - acft.yIni));
            }

            private void VerificarTrocarRumo()
            {
                Reta retaAtual = retas[indiceRetaAtual];

                if (Math.Abs(retaAtual.Fim.X - retaAtual.Ini.X) > Math.Abs(retaAtual.Fim.Y - retaAtual.Ini.Y))
                {
                    if (direita)
                    {
                        if (this.X > retaAtual.Fim.X)
                            this.AtualizarRumo();
                    }
                    else
                        if (this.X < retaAtual.Fim.X)
                            this.AtualizarRumo();
                }
                else
                {
                    if (descer)
                    {
                        if (this.Y > retaAtual.Fim.Y)
                            this.AtualizarRumo();
                    }
                    else
                        if (this.Y < retaAtual.Fim.Y)
                            this.AtualizarRumo();
                }
            }

            private void AtualizarRumo()
            {
                if (indiceRetaAtual < retas.Count - 1)
                {
                    indiceRetaAtual++;
                    Reta retaAtual = retas[indiceRetaAtual];

                    this.Trafegar(retaAtual.Fim);
                }
                else
                    timer.Enabled = false;
            }

        #endregion ////----------------------------------------------


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        #region ----------------metodos publicos-------------------
        
            public void MudarRumo(Local local)
            {
                local.donoGrupo = this.donoGrupo;
                local.donoAcft = this;
                retas[indiceRetaAtual].Fim = local;

                if (indiceRetaAtual < retas.Count - 1)
                {
                    retas[indiceRetaAtual + 1].Ini.X = retas[indiceRetaAtual].Fim.X;
                    retas[indiceRetaAtual + 1].Ini.Y = retas[indiceRetaAtual].Fim.Y;
                }

                this.Trafegar(local);
            }

            public void IncluirRetaNoFinal(Local local)
            {
                local.donoGrupo = this.donoGrupo;
                local.donoAcft = this;
                if (!locais.Contains(local))
                    locais.Add(local);

                //if (retas[retas.Count - 1].Fim.X != 0 && retas[retas.Count - 1].Fim.Y != 0)

                //if (retas[retas.Count - 1].Ini == retas[retas.Count - 1].Fim)
                if ((retas[retas.Count - 1].Ini.X == retas[retas.Count - 1].Fim.X) && (retas[retas.Count - 1].Ini.Y == retas[retas.Count - 1].Fim.Y))
                {
                    retas[retas.Count - 1].Fim = local;
                    this.Trafegar(local);
                }
                else
                {
                    Reta reta = new Reta();
                    ////reta.Ini = new Local();
                    //reta.Ini.X = retas[retas.Count - 1].Fim.X;
                    //reta.Ini.Y = retas[retas.Count - 1].Fim.Y;
                    reta.Ini = retas[retas.Count - 1].Fim;
                    reta.Fim = local;
                    retas.Add(reta);
                }
            }

            public void Mover(int X, int Y)
            {
                Reta retaAtual = retas[indiceRetaAtual];

                retaAtual.Ini.X = X;
                retaAtual.Ini.Y = Y;
                this.X = X;
                this.Y = Y;

                //if (acfts[i].timer != null)
                //    acfts[i].timer.Dispose();
                if ((retaAtual.Fim.X != 0) && (retaAtual.Fim.Y != 0))
                    this.Trafegar(retaAtual.Fim);

                this.AtualizarDadosAltitude();

                //this.VerificarDistanciaEntreACFTs(10, 200);
            }

            public void AtualizarDadosAltitude()
            {
                lblAltitude.Font = new Font("Tahoma", lblMatricula.Font.Size - 3, FontStyle.Bold);

                lblAltitude.Left = this.X;
                lblAltitude.Top = this.Y + lblMatricula.Height;

                this.AtualizarDistanciaEntreACFTs(this.distanciaLateralMin, this.distanciaVerticalMin);
            }

            //

            public void Trafegar(Local local)
            {
                Reta retaAtual = retas[indiceRetaAtual];

                retaAtual.Ini.X = this.X;
                retaAtual.Ini.Y = this.Y;

                retaAtual.Fim = local;

                espaco = Math.Sqrt(Math.Abs(retaAtual.Fim.X - retaAtual.Ini.X) + Math.Abs(retaAtual.Fim.Y - retaAtual.Ini.Y));
                tempo = espaco / Velocidade;

                //dados da equacao da reta
                retaAtual.a = retaAtual.Ini.Y - retaAtual.Fim.Y;
                retaAtual.b = retaAtual.Fim.X - retaAtual.Ini.X;
                retaAtual.c = (retaAtual.Ini.X * retaAtual.Fim.Y) - (retaAtual.Fim.X * retaAtual.Ini.Y);
                //------------------------

                direita = (retaAtual.Ini.X < retaAtual.Fim.X);
                descer = (retaAtual.Ini.Y < retaAtual.Fim.Y);

                if (timer != null)
                    timer.Dispose();

                timer = new Timer();
                timer.Enabled = true;
                timer.Interval = this.GerarTimer(Velocidade);
                //timer.Tag = this.Id;

                timer.Tick -= new EventHandler(timer_Tick);
                timer.Tick += new EventHandler(timer_Tick);

                //indiceSelecionado = -1;
                //this.mudarFundo(Color.White);
            }

            public void AtualizarDistanciaEntreACFTs(int distanciaLateralMinima, int distanciaVerticalMinima)
            {
                foreach (var obj1 in this.listaACFTs_parent.ACFTs)
                    obj1.piscarFundoAlerta = false;

                foreach (var obj1 in this.listaACFTs_parent.ACFTs)
                {
                    foreach (var obj2 in this.listaACFTs_parent.ACFTs)
                    {
                        if (obj1 != obj2)
                            if (Math.Abs(obj1.Altitude - obj2.Altitude) < distanciaVerticalMinima)
                            {
                                if (obj1.Posicao.Distancia(obj2.Posicao) < distanciaLateralMinima)
                                {
                                    obj1.piscarFundoAlerta = true;
                                    obj2.piscarFundoAlerta = true;
                                }
                            }
                    }
                }
            }

        #endregion ----------------metodos publicos-------------------

    }
}