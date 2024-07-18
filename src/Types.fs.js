import { Union, Record } from "./fable_modules/fable-library.4.0.1/Types.js";
import { union_type, record_type, list_type, string_type } from "./fable_modules/fable-library.4.0.1/Reflection.js";

export class Protocoltext extends Record {
    "constructor"(Content) {
        super();
        this.Content = Content;
    }
}

export function Protocoltext$reflection() {
    return record_type("Types.Protocoltext", [], Protocoltext, () => [["Content", list_type(string_type)]]);
}

export class Annotation extends Record {
    "constructor"(Key) {
        super();
        this.Key = Key;
    }
}

export function Annotation$reflection() {
    return record_type("Types.Annotation", [], Annotation, () => [["Key", string_type]]);
}

export class Annotations extends Record {
    "constructor"(Annotation) {
        super();
        this.Annotation = Annotation;
    }
}

export function Annotations$reflection() {
    return record_type("Types.Annotations", [], Annotations, () => [["Annotation", list_type(Annotation$reflection())]]);
}

export class Page extends Union {
    "constructor"(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Builder", "Contact", "Help"];
    }
}

export function Page$reflection() {
    return union_type("Types.Page", [], Page, () => [[], [], []]);
}

