import { class_type } from "./fable_modules/fable-library.4.0.1/Reflection.js";
import { createElement } from "react";
import React from "react";
import { createObj } from "./fable_modules/fable-library.4.0.1/Util.js";
import { Helpers_combineClasses } from "./fable_modules/Feliz.Bulma.3.0.0/./ElementBuilders.fs.js";
import { ofArray, singleton } from "./fable_modules/fable-library.4.0.1/List.js";
import { Interop_reactApi } from "./fable_modules/Feliz.2.6.0/./Interop.fs.js";

export class Builder {
    "constructor"() {
    }
}

export function Builder$reflection() {
    return class_type("Components.Builder", void 0, Builder);
}

export function Builder_Main() {
    let elems_1, elems;
    return createElement("div", createObj(Helpers_combineClasses("columns", ofArray([["className", "px-10 py-3"], (elems_1 = [createElement("div", createObj(Helpers_combineClasses("column", ofArray([["className", "is-three-fifths"], (elems = [createElement("div", createObj(Helpers_combineClasses("block", singleton(["children", "ProtocolExample.docx"])))), createElement("div", createObj(Helpers_combineClasses("block", ofArray([["className", "text-justify bg-slate-100 border-[#10242b] border-4 p-3"], ["children", "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?\r\n                                At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat."]]))))], ["children", Interop_reactApi.Children.toArray(Array.from(elems))])])))), createElement("div", createObj(Helpers_combineClasses("column", singleton(["children", "Annotations"]))))], ["children", Interop_reactApi.Children.toArray(Array.from(elems_1))])]))));
}

