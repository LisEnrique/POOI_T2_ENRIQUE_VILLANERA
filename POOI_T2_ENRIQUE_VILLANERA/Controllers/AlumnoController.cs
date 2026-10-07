using Newtonsoft.Json;
using POOI_T2_ENRIQUE_VILLANERA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace POOI_T2_ENRIQUE_VILLANERA.Controllers
{
    public class AlumnoController : Controller
    {
        static string jlistAlumnos = "[]";

        public ActionResult Index()
        {
            try
            {
                List<Alumno> listaIntermedia =
                    JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);

                return View(listaIntermedia);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View(new List<Alumno>());
            }
        }
 
        public ActionResult Agregar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Agregar(Alumno alumno)
        {
            try
            {
                // Deserializar el JSON
                List<Alumno> temporal =
                    JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);

                Alumno alumnoEncontrado =
                    temporal.Find(a => a.dni == alumno.dni);

                if (alumnoEncontrado != null)
                {
                    ViewBag.mensaje = "El DNI ya existe";
                    return View(alumno);
                }

                temporal.Add(alumno);

                // Serializar otra vez
                jlistAlumnos =
                    JsonConvert.SerializeObject(temporal);

                ViewBag.mensaje = "ALUMNO CREADO CORRECTAMENTE";

                return View(alumno);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View(alumno);
            }
        }


        public ActionResult Eliminar(string dni)
        {
            try
            {
                List<Alumno> temporal =
                    JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);

                Alumno alumnoEncontrado =
                    temporal.Find(a => a.dni == dni);

                if (alumnoEncontrado != null)
                {
                    temporal.Remove(alumnoEncontrado);

                    jlistAlumnos =
                        JsonConvert.SerializeObject(temporal);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = ex.Message;

                return RedirectToAction("Index");
            }
        }


        public ActionResult Serializar()
        {
            string mensaje = string.Empty;

            try
            {
                List<Alumno> temporal =
                    JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);

                jlistAlumnos =
                    JsonConvert.SerializeObject(temporal);

                string ruta =
                    Server.MapPath("~/App_Data/Alumnos.json");

                System.IO.File.WriteAllText(ruta, jlistAlumnos);

                mensaje =
                    "ALUMNO SERIALIZADO CORRECTAMENTE";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            TempData["mensaje"] = mensaje;

            return RedirectToAction("Index");
        }


        public ActionResult Detalles(string dni)
        {
            try
            {
                List<Alumno> temporal =
                    JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);

                Alumno alumnoEncontrado =
                    temporal.Find(a => a.dni == dni);

                return View(alumnoEncontrado);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;

                return View();
            }
        }

        public ActionResult Actualizar(string dni)
        {
            try
            {
                List<Alumno> temporal =
                    JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);

                Alumno alumnoEncontrado =
                    temporal.Find(a => a.dni == dni);

                return View(alumnoEncontrado);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;

                return View();
            }
        }


        [HttpPost]
        public ActionResult Actualizar(Alumno alumno)
        {
            try
            {
                List<Alumno> temporal =
                    JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);

                Alumno alumnoEncontrado =
                    temporal.Find(a => a.dni == alumno.dni);

                if (alumnoEncontrado != null)
                {
                    alumnoEncontrado.nombres = alumno.nombres;
                    alumnoEncontrado.apellidos = alumno.apellidos;
                    alumnoEncontrado.carrera = alumno.carrera;
                    alumnoEncontrado.ciclo = alumno.ciclo;

                    jlistAlumnos =
                        JsonConvert.SerializeObject(temporal);

                    ViewBag.mensaje =
                        "Alumno actualizado correctamente";
                }

                return View(alumno);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;

                return View(alumno);
            }
        }
    }
}