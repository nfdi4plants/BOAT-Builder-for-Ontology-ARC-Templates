import { class_type } from "./fable_modules/fable-library.4.0.1/Reflection.js";
import { createElement } from "react";
import React from "react";
import { useFeliz_React__React_useState_Static_1505 } from "./fable_modules/Feliz.2.6.0/React.fs.js";
import { Page } from "./Types.fs.js";
import { printf, toConsole } from "./fable_modules/fable-library.4.0.1/String.js";
import { createObj } from "./fable_modules/fable-library.4.0.1/Util.js";
import { empty, singleton, append, delay, toList } from "./fable_modules/fable-library.4.0.1/Seq.js";
import { NavBar_Subpagelink_Z6959EB6 } from "./NavBar.fs.js";
import { singleton as singleton_1 } from "./fable_modules/fable-library.4.0.1/List.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.6.0/./Interop.fs.js";
import { Contact_Main } from "./Contact.fs.js";
import { Help_Main } from "./Help.fs.js";
import { Builder_Main } from "./Builder.fs.js";

export class View {
    "constructor"() {
    }
}

export function View$reflection() {
    return class_type("App.View", void 0, View);
}

export function View_Main() {
    let elems;
    const patternInput = useFeliz_React__React_useState_Static_1505(new Page(0, []));
    const setpage = patternInput[1];
    const currentpage = patternInput[0];
    const clo = toConsole(printf("%A"));
    clo(currentpage);
    return createElement("div", createObj(singleton_1((elems = toList(delay(() => {
        let children;
        return append(singleton((children = singleton_1(NavBar_Subpagelink_Z6959EB6(setpage, currentpage)), createElement("a", {
            children: Interop_reactApi.Children.toArray(Array.from(children)),
        }))), delay(() => {
            const matchValue = currentpage;
            switch (matchValue.tag) {
                case 1: {
                    createElement(Contact_Main, null);
                    return empty();
                }
                case 2: {
                    createElement(Help_Main, null);
                    return empty();
                }
                default: {
                    return singleton(createElement(Builder_Main, null));
                }
            }
        }));
    })), ["children", Interop_reactApi.Children.toArray(Array.from(elems))]))));
}

