using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using System.Security.Policy;

namespace Gestion_Articulos
{
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulos;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.list();
            dgvLista.DataSource = listaArticulos;
            cargarImagen(listaArticulos[0].urlImagen);
            ocultarFilas();

        }

        private void ocultarFilas()
        {
            dgvLista.Columns["urlImagen"].Visible = false;
            
        }
        
        
        

        private void dgvLista_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvLista.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.urlImagen);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxUrlImagen.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxUrlImagen.Load("https://editorial.unc.edu.ar/wp-content/uploads/sites/33/2022/09/placeholder.png");
            }
        }
    }

    
}
