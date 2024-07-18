import { class_type } from "./fable_modules/fable-library.4.0.1/Reflection.js";
import { singleton, append, delay, toList } from "./fable_modules/fable-library.4.0.1/Seq.js";
import DataPLANT_logo_bg_transparent from "./img/DataPLANT_logo_bg_transparent.svg";
import { createElement } from "react";
import { createObj } from "./fable_modules/fable-library.4.0.1/Util.js";
import { Helpers_combineClasses } from "./fable_modules/Feliz.Bulma.3.0.0/./ElementBuilders.fs.js";
import { ofArray, singleton as singleton_1 } from "./fable_modules/fable-library.4.0.1/List.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.6.0/./Interop.fs.js";
import { Page } from "./Types.fs.js";

export class NavBar {
    "constructor"() {
    }
}

export function NavBar$reflection() {
    return class_type("Components.NavBar", void 0, NavBar);
}

export function NavBar_Subpagelink_Z6959EB6(setPage, statePage) {
    const children_6 = toList(delay(() => {
        let elems_7, elms_1, elms, elms_5, elms_2, elms_4, elms_3, elems_3;
        const logo = DataPLANT_logo_bg_transparent;
        return append(singleton(createElement("nav", createObj(Helpers_combineClasses("navbar", ofArray([["className", "navbar py-0"], (elems_7 = [(elms_1 = singleton_1((elms = singleton_1(createElement("img", {
            src: logo,
            height: 40,
            width: 150,
        })), createElement("a", {
            className: "navbar-item",
            children: Interop_reactApi.Children.toArray(Array.from(elms)),
        }))), createElement("div", {
            className: "navbar-brand",
            children: Interop_reactApi.Children.toArray(Array.from(elms_1)),
        })), (elms_5 = ofArray([(elms_2 = singleton_1(createElement("div", createObj(Helpers_combineClasses("navbar-item", singleton_1(["children", "BOAT"]))))), createElement("div", {
            className: "navbar-start",
            children: Interop_reactApi.Children.toArray(Array.from(elms_2)),
        })), (elms_4 = singleton_1((elms_3 = ofArray([createElement("a", createObj(Helpers_combineClasses("navbar-item", ofArray([["children", "Builder"], ["onClick", (_arg) => {
            setPage(new Page(0, []));
        }]])))), createElement("a", createObj(Helpers_combineClasses("navbar-item", ofArray([["children", "Restart"], ["onClick", (_arg_1) => {
        }]])))), createElement("a", createObj(Helpers_combineClasses("navbar-item", singleton_1(["children", "Download"])))), createElement("a", createObj(Helpers_combineClasses("navbar-item", ofArray([["children", "Help"], ["onClick", (_arg_2) => {
            setPage(new Page(2, []));
        }]])))), createElement("a", createObj(Helpers_combineClasses("navbar-item", ofArray([["children", "Contact"], ["onClick", (_arg_3) => {
            setPage(new Page(1, []));
        }]])))), createElement("a", createObj(Helpers_combineClasses("navbar-item", ofArray([["href", "https://github.com/Rookabu/Project-GTHelper"], ["target", "_blank"], ["className", "flex"], ["style", {
            paddingLeft: 0,
            paddingRight: 0,
        }], (elems_3 = [createElement("i", {
            className: "fa-brands fa-github",
            style: {
                fontSize: 3 + "rem",
            },
        })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_3))])]))))]), createElement("div", {
            className: "navbar-item",
            children: Interop_reactApi.Children.toArray(Array.from(elms_3)),
        }))), createElement("div", {
            className: "navbar-end",
            children: Interop_reactApi.Children.toArray(Array.from(elms_4)),
        }))]), createElement("div", {
            className: "navbar-menu",
            children: Interop_reactApi.Children.toArray(Array.from(elms_5)),
        }))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_7))])]))))), delay(() => {
            let elems_9, elems_8;
            return singleton(createElement("footer", createObj(Helpers_combineClasses("footer", ofArray([["className", "content has-text-centered footer"], (elems_9 = [createElement("div", createObj(ofArray([["className", "inline-flex"], (elems_8 = [createElement("p", {
                children: "This is a footer. By",
                className: "pr-1",
            }), createElement("a", {
                children: "ndfdi4plants",
                href: "https://www.nfdi4plants.de/",
                target: "_blank",
                className: "underline text-blue-400",
            })], ["children", Interop_reactApi.Children.toArray(Array.from(elems_8))])])))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_9))])])))));
        }));
    }));
    return createElement("div", {
        children: Interop_reactApi.Children.toArray(Array.from(children_6)),
    });
}

