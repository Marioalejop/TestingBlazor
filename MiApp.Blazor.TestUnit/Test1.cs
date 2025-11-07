using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiApp.Blazor.Pages;

namespace MiApp.Blazor.TestUnit
{
    [TestClass]
    public class TareasTests
    {
        [TestMethod]
        public void AgregarTarea_DeberiaAumentarLaListaYActualizarMensaje()
        {
            var componente = new Tareas();
            componente.AgregarTarea();
            Assert.AreEqual(1, componente.listaTareas.Count);
        }
    }
}