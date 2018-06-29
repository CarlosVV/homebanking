declare namespace imagepicker {
    export interface IImagePicker {
        opts: IImagePickerOptions;
        multiple: boolean;
        picker: JQuery;
        picker_options: IImagePickerOption[];
        select: JQuery;
        has_implicit_blanks: boolean;
        selected_values: JQuery[];
        destroy(): JQuery;
        build_and_append_picker(): JQuery[];
        sync_picker_with_select(): JQuery[];
        create_picker(): JQuery;
        recursively_parse_option_groups(): IImagePickerOption[];
        toggle(imagepicker_option: IImagePickerOption): void;
    }

    export interface IImagePickerOption {
        picker: IImagePicker;
        opts: IImagePickerOptions;
        option: JQuery;
        node: JQuery;
        destroy(): JQuery;
        has_image(): boolean;
        is_blank(): boolean;
        is_selected(): boolean;
        mark_as_selected(): JQuery;
        unmark_as_selected(): JQuery;
        value(): any;
        label(): string;
        clicked(): void;
        create_node(): JQuery;
    }

    export interface IImagePickerOptions {
        hide_select?: boolean;
        show_label?: boolean;
        initialized?: () => void;
        changed?: (oldValues: string[], selectedValues: string[]) => void;
        clicked?: (option: IImagePickerOption) => void;
        selected?: (option: IImagePickerOption) => void;
        limit?: number;
        limit_reached?: () => void;
    }
}

interface JQuery { // tslint:disable-line:interface-name
    imagepicker(opts: imagepicker.IImagePickerOptions): JQuery;
}