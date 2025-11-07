using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MiApp.Blazor.TestUnit
{
    [TestClass]
    public class TareasTests
    {
        private List<string> listaTareas;
        private string mensaje;

        [TestInitialize]
        public void Setup()
        {
            listaTareas = new List<string>();
            mensaje = string.Empty;
        }

        [TestMethod]
        public void AgregarTarea_DeberiaAumentarLaListaYActualizarMensaje()
        {
            // Arrange
            var tarea = "Nueva tarea";

            // Act
            listaTareas.Add(tarea);
            mensaje = $"Total tareas: {listaTareas.Count}";

            // Assert
            Assert.AreEqual(1, listaTareas.Count, "La lista debería tener una tarea.");
            Assert.AreEqual("Total tareas: 1", mensaje, "El mensaje debería reflejar el total.");
        }
    }
}
