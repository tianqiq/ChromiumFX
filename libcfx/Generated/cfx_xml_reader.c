// Copyright (c) 2014-2017 Wolfgang Borgsmüller
// All rights reserved.
// 
// This software may be modified and distributed under the terms
// of the BSD license. See the License.txt file for details.

// Generated file. Do not edit.


// cef_xml_reader

// CEF_EXPORT cef_xml_reader_t* cef_xml_reader_create(cef_stream_reader_t* stream, cef_xml_encoding_type_t encodingType, const cef_string_t* URI);
static cef_xml_reader_t* cfx_xml_reader_create(cef_stream_reader_t* stream, cef_xml_encoding_type_t encodingType, char16 *URI_str, int URI_length) {
    if(stream) ((cef_base_ref_counted_t*)stream)->add_ref((cef_base_ref_counted_t*)stream);
    cef_string_t URI = { URI_str, URI_length, 0 };
    return cef_xml_reader_create(stream, encodingType, &URI);
}
// move_to_next_node
static int cfx_xml_reader_move_to_next_node(cef_xml_reader_t* self) {
    return self->move_to_next_node(self);
}

// close
static int cfx_xml_reader_close(cef_xml_reader_t* self) {
    return self->close(self);
}

// has_error
static int cfx_xml_reader_has_error(cef_xml_reader_t* self) {
    return self->has_error(self);
}

// get_error
static cef_string_userfree_t cfx_xml_reader_get_error(cef_xml_reader_t* self) {
    return self->get_error(self);
}

// get_type
static cef_xml_node_type_t cfx_xml_reader_get_type(cef_xml_reader_t* self) {
    return self->get_type(self);
}

// get_depth
static int cfx_xml_reader_get_depth(cef_xml_reader_t* self) {
    return self->get_depth(self);
}

// get_local_name
static cef_string_userfree_t cfx_xml_reader_get_local_name(cef_xml_reader_t* self) {
    return self->get_local_name(self);
}

// get_prefix
static cef_string_userfree_t cfx_xml_reader_get_prefix(cef_xml_reader_t* self) {
    return self->get_prefix(self);
}

// get_qualified_name
static cef_string_userfree_t cfx_xml_reader_get_qualified_name(cef_xml_reader_t* self) {
    return self->get_qualified_name(self);
}

// get_namespace_uri
static cef_string_userfree_t cfx_xml_reader_get_namespace_uri(cef_xml_reader_t* self) {
    return self->get_namespace_uri(self);
}

// get_base_uri
static cef_string_userfree_t cfx_xml_reader_get_base_uri(cef_xml_reader_t* self) {
    return self->get_base_uri(self);
}

// get_xml_lang
static cef_string_userfree_t cfx_xml_reader_get_xml_lang(cef_xml_reader_t* self) {
    return self->get_xml_lang(self);
}

// is_empty_element
static int cfx_xml_reader_is_empty_element(cef_xml_reader_t* self) {
    return self->is_empty_element(self);
}

// has_value
static int cfx_xml_reader_has_value(cef_xml_reader_t* self) {
    return self->has_value(self);
}

// get_value
static cef_string_userfree_t cfx_xml_reader_get_value(cef_xml_reader_t* self) {
    return self->get_value(self);
}

// has_attributes
static int cfx_xml_reader_has_attributes(cef_xml_reader_t* self) {
    return self->has_attributes(self);
}

// get_attribute_count
static size_t cfx_xml_reader_get_attribute_count(cef_xml_reader_t* self) {
    return self->get_attribute_count(self);
}

// get_attribute_byindex
static cef_string_userfree_t cfx_xml_reader_get_attribute_byindex(cef_xml_reader_t* self, int index) {
    return self->get_attribute_byindex(self, index);
}

// get_attribute_byqname
static cef_string_userfree_t cfx_xml_reader_get_attribute_byqname(cef_xml_reader_t* self, char16 *qualifiedName_str, int qualifiedName_length) {
    cef_string_t qualifiedName = { qualifiedName_str, qualifiedName_length, 0 };
    return self->get_attribute_byqname(self, &qualifiedName);
}

// get_attribute_bylname
static cef_string_userfree_t cfx_xml_reader_get_attribute_bylname(cef_xml_reader_t* self, char16 *localName_str, int localName_length, char16 *namespaceURI_str, int namespaceURI_length) {
    cef_string_t localName = { localName_str, localName_length, 0 };
    cef_string_t namespaceURI = { namespaceURI_str, namespaceURI_length, 0 };
    return self->get_attribute_bylname(self, &localName, &namespaceURI);
}

// get_inner_xml
static cef_string_userfree_t cfx_xml_reader_get_inner_xml(cef_xml_reader_t* self) {
    return self->get_inner_xml(self);
}

// get_outer_xml
static cef_string_userfree_t cfx_xml_reader_get_outer_xml(cef_xml_reader_t* self) {
    return self->get_outer_xml(self);
}

// get_line_number
static int cfx_xml_reader_get_line_number(cef_xml_reader_t* self) {
    return self->get_line_number(self);
}

// move_to_attribute_byindex
static int cfx_xml_reader_move_to_attribute_byindex(cef_xml_reader_t* self, int index) {
    return self->move_to_attribute_byindex(self, index);
}

// move_to_attribute_byqname
static int cfx_xml_reader_move_to_attribute_byqname(cef_xml_reader_t* self, char16 *qualifiedName_str, int qualifiedName_length) {
    cef_string_t qualifiedName = { qualifiedName_str, qualifiedName_length, 0 };
    return self->move_to_attribute_byqname(self, &qualifiedName);
}

// move_to_attribute_bylname
static int cfx_xml_reader_move_to_attribute_bylname(cef_xml_reader_t* self, char16 *localName_str, int localName_length, char16 *namespaceURI_str, int namespaceURI_length) {
    cef_string_t localName = { localName_str, localName_length, 0 };
    cef_string_t namespaceURI = { namespaceURI_str, namespaceURI_length, 0 };
    return self->move_to_attribute_bylname(self, &localName, &namespaceURI);
}

// move_to_first_attribute
static int cfx_xml_reader_move_to_first_attribute(cef_xml_reader_t* self) {
    return self->move_to_first_attribute(self);
}

// move_to_next_attribute
static int cfx_xml_reader_move_to_next_attribute(cef_xml_reader_t* self) {
    return self->move_to_next_attribute(self);
}

// move_to_carrying_element
static int cfx_xml_reader_move_to_carrying_element(cef_xml_reader_t* self) {
    return self->move_to_carrying_element(self);
}


