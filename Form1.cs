using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace gerenciados_atalhos
{
    public partial class telaPrincipal : Form
    {
        public telaPrincipal()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            String cComandos = "-m -P=%PROGRAMA% -C=%CONEXAO% -E=%AMBIENTE%";
            String cProgExec = "";
            String cTipoAmbiente = "";
            String cPrograma = "";
            String cConexao = "";
            String cAmbiente = "";
            int nIndexTipoAmbiente = cBoxTipoAmbiente.SelectedIndex;
            int nIndexAmbiente = cBoxAmbiente.SelectedIndex;
            int nIndexConexao = cBoxConexao.SelectedIndex;
            int nIndexPrograma = cBoxPrograma.SelectedIndex;

            if (nIndexTipoAmbiente >= 0 && nIndexAmbiente >= 0 && nIndexConexao >= 0 && nIndexPrograma >= 0)
            {
                cTipoAmbiente = cBoxTipoAmbiente.SelectedItem.ToString();
                cPrograma = cBoxPrograma.SelectedItem.ToString();
                cAmbiente = cBoxAmbiente.SelectedItem.ToString();
                cConexao = cBoxConexao.SelectedItem.ToString();

                if (cTipoAmbiente == "PRODUCAO")
                {
                    cProgExec = "C:\\Totvs\\Protheus12_prd\\smartclient.exe";
                }
                else if (cTipoAmbiente == "DESENVOLVIMENTO")
                {
                    cProgExec = "C:\\Totvs\\Protheus12_DEV\\smartclient.exe";
                }
                else if (cTipoAmbiente == "HOMOLOGACAO")
                {
                    cProgExec = "C:\\Totvs\\Protheus12_HOM\\smartclient.exe";
                }
                else if (cTipoAmbiente == "VENDAS")
                {
                    cProgExec = "C:\\Totvs\\Protheus12_VEN\\smartclient.exe";
                }
                else
                {
                    cProgExec = "C:\\Totvs\\Protheus11_tkm\\SmartClient.exe";
                }

                if (cPrograma == "MATRIZ DE VERSATILIDADE")
                {
                    cPrograma = "u_PZY3VIEW";
                }
                else if (cPrograma == "CHAMADOS DE TI")
                {
                    cPrograma = "u_PCHMDSTI";
                }
                else if (cPrograma == "TELNET")
                {
                    cPrograma = "VTDEBUG";
                    cAmbiente = "PARANOA";
                }

                if (cAmbiente == "SPLASH")
                {
                    cComandos = "-m";
                }
                else
                {
                    cComandos = cComandos.Replace("%PROGRAMA%", cPrograma);
                    cComandos = cComandos.Replace("%CONEXAO%", cConexao);
                    cComandos = cComandos.Replace("%AMBIENTE%", cAmbiente);
                }

                if (File.Exists(cProgExec))
                {

                    System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                    pProcess.StartInfo.FileName = cProgExec;
                    pProcess.StartInfo.Arguments = cComandos;
                    pProcess.Start();
                    telaPrincipal.ActiveForm.Close();


                }
                else
                {
                    MessageBox.Show("Programa não encontrado! \r\n" + cProgExec, "ATALHOS TOTVS-PROTHEUS");
                }

            }
            else
            {
                MessageBox.Show("Selecionar todos os parametros para execução", "ATALHOS TOTVS-PROTHEUS");
            }

        }

        private void cBoxTipoAmbiente_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            cBoxPrograma.Items.Clear();
            cBoxConexao.Items.Clear();
            cBoxAmbiente.Items.Clear();

            cBoxPrograma.Items.Add("SIGAMDI");
            cBoxPrograma.Items.Add("SIGAADV");

            if (cBoxTipoAmbiente.SelectedItem.ToString() == "PRODUCAO")
            {
                cBoxPrograma.Items.Add("CHAMADOS DE TI");
                cBoxPrograma.Items.Add("MATRIZ DE VERSATILIDADE");
                cBoxPrograma.Items.Add("VTDEBUG");
            }

        }

        private void cBoxPrograma_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            cBoxConexao.Items.Clear();
            cBoxConexao.Enabled = true;
            cBoxAmbiente.Items.Clear();
            cBoxAmbiente.Enabled = true;

            if (cBoxPrograma.SelectedItem.ToString() == "CHAMADOS DE TI" || cBoxPrograma.SelectedItem.ToString() == "MATRIZ DE VERSATILIDADE")
            {
                cBoxConexao.Items.Add("TCP");
                cBoxConexao.SelectedIndex = 0;
                cBoxConexao.Enabled = false;

                cBoxAmbiente.Items.Add("PARANOA");
                cBoxAmbiente.SelectedIndex = 0;
                cBoxAmbiente.Enabled = false;
            }

            else if (cBoxPrograma.SelectedItem.ToString() == "SIGAADV" || cBoxPrograma.SelectedItem.ToString() == "SIGAMDI" || cBoxPrograma.SelectedItem.ToString() == "VTDEBUG")
            {

                cBoxConexao.Items.Add("TCP");
                cBoxConexao.Items.Add("EXTERNO");
                cBoxConexao.SelectedIndex = 0;

                if (cBoxTipoAmbiente.SelectedItem.ToString() == "PRODUCAO")
                {
                    cBoxConexao.Items.Add("SERVICE10");
                    cBoxConexao.Items.Add("SERVICE15");
                    cBoxConexao.Items.Add("SERVICE16");
                    cBoxConexao.Items.Add("SERVICE17");
                    cBoxConexao.Items.Add("SERVICE18");
                    cBoxConexao.Items.Add("SERVICE19");
                    cBoxConexao.Items.Add("SERVICE20");
                    cBoxConexao.Items.Add("SERVICE26");
                    cBoxConexao.Items.Add("SERVICE27");
                    cBoxConexao.Items.Add("SERVICE28");
                    cBoxConexao.Items.Add("BROKER");
                }
            }

        }

        private void cBoxConexao_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            bool lSplash = true;

            cBoxAmbiente.Items.Clear();
            cBoxAmbiente.Enabled = true;

            if (cBoxTipoAmbiente.SelectedItem.ToString() == "PRODUCAO")
            {
                if (cBoxConexao.SelectedItem.ToString() == "TELNET")
                {
                    cBoxAmbiente.Items.Add("TELNET");
                }
                else if (cBoxConexao.SelectedItem.ToString() == "TCP" || cBoxConexao.SelectedItem.ToString() == "EXTERNO")
                {
                    cBoxAmbiente.Items.Add("PARANOA");
                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE10")
                {
                    cBoxAmbiente.Items.Add("PARANOA");
                    cBoxAmbiente.Items.Add("PARANOA_CST");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE15")
                {
                    cBoxAmbiente.Items.Add("ANALISTA_E");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE16")
                {
                    cBoxAmbiente.Items.Add("ANALISTA_I");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE17")
                {
                    cBoxAmbiente.Items.Add("ANALISTA_J");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE18")
                {
                    cBoxAmbiente.Items.Add("ANALISTA_R");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE19")
                {
                    cBoxAmbiente.Items.Add("ANALISTA_S");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE20")
                {
                    cBoxAmbiente.Items.Add("ANALISTA_M");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE26")
                {
                    cBoxAmbiente.Items.Add("ANALISTA_RK");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "SERVICE27")
                {
                    cBoxAmbiente.Items.Add("ANALISTA_A");

                }
                else if (cBoxConexao.SelectedItem.ToString() == "BROKER")
                {
                    cBoxAmbiente.Items.Add("PARANOA");
                    cBoxAmbiente.Enabled = false;
                    lSplash = false;

                }

            }
            else if (cBoxTipoAmbiente.SelectedItem.ToString() == "DESENVOLVIMENTO")
            {
                cBoxAmbiente.Items.Add("DESENV");
            }
            else if (cBoxTipoAmbiente.SelectedItem.ToString() == "HOMOLOGACAO")
            {
                cBoxAmbiente.Items.Add("HOMOLOG");
            }
            else if (cBoxTipoAmbiente.SelectedItem.ToString() == "VENDAS")
            {
                cBoxAmbiente.Items.Add("VENDAS");
            }
            else if (cBoxTipoAmbiente.SelectedItem.ToString() == "TKM-HISTORICO")
            {
                cBoxAmbiente.Items.Add("P11");
            }

            if (lSplash)
            {
                cBoxAmbiente.Items.Add("SPLASH");
            }
            cBoxAmbiente.SelectedIndex = 0;
        }

    }
}
