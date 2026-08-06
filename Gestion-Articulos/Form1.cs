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
            cargarFormulario();
            cboTipoFiltro.Items.Add("Codigo");
            cboTipoFiltro.Items.Add("Nombre");
            cboTipoFiltro.Items.Add("Precio");

        }
        private void cargarFormulario()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.list();
                dgvLista.DataSource = listaArticulos;
                dgvLista.Columns["precio"].DefaultCellStyle.Format = "N2";
                ocultarFilas();
                cargarImagen(listaArticulos[0].urlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarFilas()
        {
            dgvLista.Columns["urlImagen"].Visible = false;
            dgvLista.Columns["id"].Visible = false;
            
        }
        

        private void dgvLista_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvLista.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvLista.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.urlImagen);

            }
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Ventana_agregar agregar = new Ventana_agregar();
            agregar.ShowDialog();
            cargarFormulario();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado = (Articulo)dgvLista.CurrentRow.DataBoundItem;
            DialogResult  respuesta = MessageBox.Show("¿Quieres eliminar : " + seleccionado.nombre, "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(respuesta == DialogResult.Yes)
            {

                negocio.eliminar(seleccionado);
                cargarFormulario();
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado =(Articulo)dgvLista.CurrentRow.DataBoundItem;
            Ventana_agregar modificar = new Ventana_agregar(seleccionado);
            modificar.ShowDialog();
            cargarFormulario();
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
           

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> articuloFiltrado;
            string filtro = txtFiltro.Text;
            string filtroItem = cboTipoFiltro.SelectedItem.ToString();
            if (filtro != null)
            {
                if (filtroItem == "Codigo")
                {
                    articuloFiltrado = listaArticulos.FindAll(x => x.codigoArticulo.ToUpper().Contains(filtro.ToUpper()));

                }
                else if (filtro.Length >= 3 && filtroItem == "Nombre")
                {
                    articuloFiltrado = listaArticulos.FindAll(x => x.nombre.ToUpper().Contains(filtro.ToUpper()));
                }
                else if (filtroItem == "Precio" && decimal.TryParse(filtro, out decimal precioFiltrado))
                {
                    articuloFiltrado = listaArticulos.FindAll(x => x.precio <= precioFiltrado);
                }
                else
                {
                    articuloFiltrado = listaArticulos;
                }
            }
            else
            {
                articuloFiltrado = listaArticulos;
            }
             
            dgvLista.DataSource = articuloFiltrado;
            ocultarFilas();
            dgvLista.Columns["precio"].DefaultCellStyle.Format = "N2";
            
            
           

            
        }
    }

    
}
