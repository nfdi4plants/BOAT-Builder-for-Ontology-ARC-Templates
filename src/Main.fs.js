import { createRoot } from "react-dom/client";
import { createElement } from "react";
import { View_Main } from "./View.fs.js";
import "./styling.scss";


export const root = createRoot(document.getElementById("feliz-app"));

root.render(createElement(View_Main, null));

