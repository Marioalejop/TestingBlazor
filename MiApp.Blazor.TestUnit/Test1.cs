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
            // Arrange
            var pagina = new Tareas();

            // Act
            pagina.AgregarTarea();

            // Assert
            Assert.AreEqual(1, pagina.listaTareas.Count);
            Assert.AreEqual("Total tareas: 1", pagina.mensaje);
        }
    }
}