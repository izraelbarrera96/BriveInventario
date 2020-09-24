// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

async function inventarioFetch(Method, Action, BodyJSON) {

    let JsonResult;
    let Response;

    if (Action === "GET") {

        let ParametrosURL = "?";

        Object.keys(BodyJSON).forEach((key) => {
            ParametrosURL += key + "=" + BodyJSON[key] + "&";
        });

        ParametrosURL = ParametrosURL.substring(0, ParametrosURL.length - 1);

        try {
            Response = await fetch(Method + ParametrosURL);
            if (Response.ok) {
                JsonResult = await Response.json();
            } else {
                JsonResult = { Resultado: false, Mensaje: "Error de Consulta" };
            }
        } catch (e) {
            JsonResult = { Resultado: false, Mensaje: "Error de Consulta" };
        }

    } else {

        let OpcionesFetch = {
            method: Action,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(BodyJSON)
        };

        try {
            Response = await fetch(Method, OpcionesFetch);
            if (Response.ok) {
                JsonResult = await Response.json();
            } else {
                JsonResult = { Resultado: false, Mensaje: "Error de Consulta" };
            }
        } catch (e) {
            JsonResult = { Resultado: false, Mensaje: "Error de Consulta" };
        }
    }

    return JsonResult;
}