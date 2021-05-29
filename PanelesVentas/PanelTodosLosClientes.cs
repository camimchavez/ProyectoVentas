using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Ventas;
using MVC;
using ServicioVentas;

namespace Paneles
{
    public partial class PanelTodosLosClientes : UserControl, IPanelVentas
    {
        public event EventHandler<VistaEventArgs> EventoObtenerTodosLosClientes;

//        IModeloVentas modelo;
        private IContratoDelServicioVentas modelo;
        private IControladorVentas controlador;

        public PanelTodosLosClientes()
        {
            InitializeComponent();
            ArmarTabla();
            // Crear una copia del evento para evitar un race condition
            // (más de un proceso tratando de usar el mismo evento)
            EventHandler<VistaEventArgs> manejador = EventoObtenerTodosLosClientes;
            if (manejador != null)
            {
                VistaEventArgs args = new VistaEventArgs("");
                manejador(this, args);
            }


        }

        private void ArmarTabla()
        {
            dgvTodosLosClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTodosLosClientes.ColumnCount = 3;

            dgvTodosLosClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvTodosLosClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTodosLosClientes.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgvTodosLosClientes.Font, FontStyle.Bold);

            dgvTodosLosClientes.Name = "dgvTodosLosClientes";
            dgvTodosLosClientes.Location = new Point(8, 8);
            dgvTodosLosClientes.Size = new Size(500, 250);
            dgvTodosLosClientes.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvTodosLosClientes.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dgvTodosLosClientes.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvTodosLosClientes.GridColor = Color.Black;
            dgvTodosLosClientes.RowHeadersVisible = false;

            dgvTodosLosClientes.Columns[0].Name = "DNI";
            dgvTodosLosClientes.Columns[1].Name = "Nombre";
            dgvTodosLosClientes.Columns[2].Name = "Dirección";

            dgvTodosLosClientes.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvTodosLosClientes.MultiSelect = false;
            dgvTodosLosClientes.Dock = DockStyle.Fill;
        }

        #region IPanelVentas Members

        public void RegistrarControlador(Ventas.IControladorVentas controlador)
        {
            this.controlador = controlador;
        }

        //public void RegistrarModelo(Ventas.IModeloVentas modelo)
        //{
        //    this.modelo = modelo;
        //}

        public void RegistrarModelo(IContratoDelServicioVentas modelo)
        {
            this.modelo = modelo;
        }

        public void MostrarPorPantalla(object obj)
        {
                Refrescar();
        }

        public void Refrescar()
        {
            try
            {
                Cliente[] lista = modelo.ObtenerTodosLosClientes();
                RefrescarTodosLosClientes(lista);
            }
            catch (Exception e)
            {
                if (modelo == null)
                {
                    throw new ExcepcionVentas("No se configuró el modelo");
                }
                else
                {
                    Console.WriteLine("Error al refrescar todos los clientes: " + e.Message);
                }
            }
        }

        #endregion

        public void RefrescarTodosLosClientes(Cliente[] lista)
        {
            dgvTodosLosClientes.Rows.Clear();
            foreach (Cliente fila in lista)
            {
            List<string> listaAVector = new List<string>();
                listaAVector.Add(fila.Id);
                listaAVector.Add(fila.Nombre);
                listaAVector.Add(fila.Direccion);
                dgvTodosLosClientes.Rows.Add(listaAVector.ToArray<string>());
            }
        }
    }
}
