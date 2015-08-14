// Copyright (c) 2013 Marshall A. Greenblatt. All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//    * Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the
// distribution.
//    * Neither the name of Google Inc. nor the name Chromium Embedded
// Framework nor the names of its contributors may be used to endorse
// or promote products derived from this software without specific prior
// written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#ifndef CEF_INCLUDE_INTERNAL_CEF_TYPES_WRAPPERS_H_
#define CEF_INCLUDE_INTERNAL_CEF_TYPES_WRAPPERS_H_
#pragma once

#include "include/internal/cef_string.h"
#include "include/internal/cef_string_list.h"
#include "include/internal/cef_types.h"

///
// Template class that provides common functionality for CEF structure wrapping.
///
template <class traits>
class CefStructBase : public traits::struct_type {
 public:
  typedef typename traits::struct_type struct_type;

  CefStructBase() : attached_to_(NULL) {
    Init();
  }
  virtual ~CefStructBase() {
    // Only clear this object's data if it isn't currently attached to a
    // structure.
    if (!attached_to_)
      Clear(this);
  }

  CefStructBase(const CefStructBase& r) {
    Init();
    *this = r;
  }
  CefStructBase(const struct_type& r) {  // NOLINT(runtime/explicit)
    Init();
    *this = r;
  }

  ///
  // Clear this object's values.
  ///
  void Reset() {
    Clear(this);
    Init();
  }

  ///
  // Attach to the source structure's existing values. DetachTo() can be called
  // to insert the values back into the existing structure.
  ///
  void AttachTo(struct_type& source) {
    // Only clear this object's data if it isn't currently attached to a
    // structure.
    if (!attached_to_)
      Clear(this);

    // This object is now attached to the new structure.
    attached_to_ = &source;

    // Transfer ownership of the values from the source structure.
    memcpy(static_cast<struct_type*>(this), &source, sizeof(struct_type));
  }

  ///
  // Relinquish ownership of values to the target structure.
  ///
  void DetachTo(struct_type& target) {
    if (attached_to_ != &target) {
      // Clear the target structure's values only if we are not currently
      // attached to that structure.
      Clear(&target);
    }

    // Transfer ownership of the values to the target structure.
    memcpy(&target, static_cast<struct_type*>(this), sizeof(struct_type));

    // Remove the references from this object.
    Init();
  }

  ///
  // Set this object's values. If |copy| is true the source structure's values
  // will be copied instead of referenced.
  ///
  void Set(const struct_type& source, bool copy) {
    traits::set(&source, this, copy);
  }

  CefStructBase& operator=(const CefStructBase& s) {
    return operator=(static_cast<const struct_type&>(s));
  }

  CefStructBase& operator=(const struct_type& s) {
    Set(s, true);
    return *this;
  }

 protected:
  void Init() {
    memset(static_cast<struct_type*>(this), 0, sizeof(struct_type));
    attached_to_ = NULL;
    traits::init(this);
  }

  static void Clear(struct_type* s) { traits::clear(s); }

  struct_type* attached_to_;
};


struct CefPointTraits {
  typedef cef_point_t struct_type;

  static inline void init(struct_type* s) {}
  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    *target = *src;
  }
};

///
// Class representing a point.
///
class CefPoint : public CefStructBase<CefPointTraits> {
 public:
  typedef CefStructBase<CefPointTraits> parent;

  CefPoint() : parent() {}
  CefPoint(const cef_point_t& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefPoint(const CefPoint& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefPoint(int x, int y) : parent() {
    Set(x, y);
  }

  bool IsEmpty() const { return x <= 0 && y <= 0; }
  void Set(int x, int y) {
    this->x = x, this->y = y;
  }
};

inline bool operator==(const CefPoint& a, const CefPoint& b) {
  return a.x == b.x && a.y == b.y;
}

inline bool operator!=(const CefPoint& a, const CefPoint& b) {
  return !(a == b);
}


struct CefRectTraits {
  typedef cef_rect_t struct_type;

  static inline void init(struct_type* s) {}
  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    *target = *src;
  }
};

///
// Class representing a rectangle.
///
class CefRect : public CefStructBase<CefRectTraits> {
 public:
  typedef CefStructBase<CefRectTraits> parent;

  CefRect() : parent() {}
  CefRect(const cef_rect_t& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefRect(const CefRect& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefRect(int x, int y, int width, int height) : parent() {
    Set(x, y, width, height);
  }

  bool IsEmpty() const { return width <= 0 || height <= 0; }
  void Set(int x, int y, int width, int height) {
    this->x = x, this->y = y, this->width = width, this->height = height;
  }
};

inline bool operator==(const CefRect& a, const CefRect& b) {
  return a.x == b.x && a.y == b.y && a.width == b.width && a.height == b.height;
}

inline bool operator!=(const CefRect& a, const CefRect& b) {
  return !(a == b);
}


struct CefSizeTraits {
  typedef cef_size_t struct_type;

  static inline void init(struct_type* s) {}
  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    *target = *src;
  }
};

///
// Class representing a size.
///
class CefSize : public CefStructBase<CefSizeTraits> {
 public:
  typedef CefStructBase<CefSizeTraits> parent;

  CefSize() : parent() {}
  CefSize(const cef_size_t& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefSize(const CefSize& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefSize(int width, int height) : parent() {
    Set(width, height);
  }

  bool IsEmpty() const { return width <= 0 || height <= 0; }
  void Set(int width, int height) {
    this->width = width, this->height = height;
  }
};

inline bool operator==(const CefSize& a, const CefSize& b) {
  return a.width == b.width && a.height == b.height;
}

inline bool operator!=(const CefSize& a, const CefSize& b) {
  return !(a == b);
}


struct CefDraggableRegionTraits {
  typedef cef_draggable_region_t struct_type;

  static inline void init(struct_type* s) {}
  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    *target = *src;
  }
};

///
// Class representing a draggable region.
///
class CefDraggableRegion : public CefStructBase<CefDraggableRegionTraits> {
 public:
  typedef CefStructBase<CefDraggableRegionTraits> parent;

  CefDraggableRegion() : parent() {}
  CefDraggableRegion(const cef_draggable_region_t& r)  // NOLINT(runtime/explicit)
    : parent(r) {}
  CefDraggableRegion(const CefDraggableRegion& r)  // NOLINT(runtime/explicit)
    : parent(r) {}
  CefDraggableRegion(const CefRect& bounds, bool draggable) : parent() {
    Set(bounds, draggable);
  }

  void Set(const CefRect& bounds, bool draggable) {
    this->bounds = bounds, this->draggable = draggable;
  }
};

inline bool operator==(const CefDraggableRegion& a,
    const CefDraggableRegion& b) {
  return a.bounds == b.bounds && a.draggable == b.draggable;
}

inline bool operator!=(const CefDraggableRegion& a,
    const CefDraggableRegion& b) {
  return !(a == b);
}


struct CefScreenInfoTraits {
  typedef cef_screen_info_t struct_type;

  static inline void init(struct_type* s) {}

  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    target->device_scale_factor = src->device_scale_factor;
    target->depth = src->depth;
    target->depth_per_component = src->depth_per_component;
    target->is_monochrome = src->is_monochrome;
    target->rect = src->rect;
    target->available_rect = src->available_rect;
  }
};

///
// Class representing the virtual screen information for use when window
// rendering is disabled.
///
class CefScreenInfo : public CefStructBase<CefScreenInfoTraits> {
 public:
  typedef CefStructBase<CefScreenInfoTraits> parent;

  CefScreenInfo() : parent() {}
  CefScreenInfo(const cef_screen_info_t& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefScreenInfo(const CefScreenInfo& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefScreenInfo(float device_scale_factor,
                int depth,
                int depth_per_component,
                bool is_monochrome,
                const CefRect& rect,
                const CefRect& available_rect) : parent() {
    Set(device_scale_factor, depth, depth_per_component,
        is_monochrome, rect, available_rect);
  }

  void Set(float device_scale_factor,
           int depth,
           int depth_per_component,
           bool is_monochrome,
           const CefRect& rect,
           const CefRect& available_rect) {
    this->device_scale_factor = device_scale_factor;
    this->depth = depth;
    this->depth_per_component = depth_per_component;
    this->is_monochrome = is_monochrome;
    this->rect = rect;
    this->available_rect = available_rect;
  }
};


struct CefKeyEventTraits {
  typedef cef_key_event_t struct_type;

  static inline void init(struct_type* s) {}

  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    target->type = src->type;
    target->modifiers = src->modifiers;
    target->windows_key_code = src->windows_key_code;
    target->native_key_code = src->native_key_code;
    target->is_system_key = src->is_system_key;
    target->character = src->character;
    target->unmodified_character = src->unmodified_character;
    target->focus_on_editable_field = src->focus_on_editable_field;
  }
};

///
// Class representing a a keyboard event.
///
typedef CefStructBase<CefKeyEventTraits> CefKeyEvent;


struct CefMouseEventTraits {
  typedef cef_mouse_event_t struct_type;

  static inline void init(struct_type* s) {}

  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    target->x = src->x;
    target->y = src->y;
    target->modifiers = src->modifiers;
  }
};

///
// Class representing a mouse event.
///
typedef CefStructBase<CefMouseEventTraits> CefMouseEvent;


struct CefPopupFeaturesTraits {
  typedef cef_popup_features_t struct_type;

  static inline void init(struct_type* s) {
    s->menuBarVisible = true;
    s->statusBarVisible = true;
    s->toolBarVisible = true;
    s->locationBarVisible = true;
    s->scrollbarsVisible = true;
    s->resizable = true;
  }

  static inline void clear(struct_type* s) {
    if (s->additionalFeatures)
      cef_string_list_free(s->additionalFeatures);
  }

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    if (target->additionalFeatures)
      cef_string_list_free(target->additionalFeatures);
    target->additionalFeatures = src->additionalFeatures ?
        cef_string_list_copy(src->additionalFeatures) : NULL;

    target->x = src->x;
    target->xSet = src->xSet;
    target->y = src->y;
    target->ySet = src->ySet;
    target->width = src->width;
    target->widthSet = src->widthSet;
    target->height = src->height;
    target->heightSet = src->heightSet;
    target->menuBarVisible = src->menuBarVisible;
    target->statusBarVisible = src->statusBarVisible;
    target->toolBarVisible = src->toolBarVisible;
    target->locationBarVisible = src->locationBarVisible;
    target->scrollbarsVisible = src->scrollbarsVisible;
    target->resizable = src->resizable;
    target->fullscreen = src->fullscreen;
    target->dialog = src->dialog;
  }
};

///
// Class representing popup window features.
///
typedef CefStructBase<CefPopupFeaturesTraits> CefPopupFeatures;


struct CefSettingsTraits {
  typedef cef_settings_t struct_type;

  static inline void init(struct_type* s) {
    s->size = sizeof(struct_type);
  }

  static inline void clear(struct_type* s) {
    cef_string_clear(&s->browser_subprocess_path);
    cef_string_clear(&s->cache_path);
    cef_string_clear(&s->user_data_path);
    cef_string_clear(&s->user_agent);
    cef_string_clear(&s->product_version);
    cef_string_clear(&s->locale);
    cef_string_clear(&s->log_file);
    cef_string_clear(&s->javascript_flags);
    cef_string_clear(&s->resources_dir_path);
    cef_string_clear(&s->locales_dir_path);
    cef_string_clear(&s->accept_language_list);
  }

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    target->single_process = src->single_process;
    target->no_sandbox = src->no_sandbox;
    cef_string_set(src->browser_subprocess_path.str,
        src->browser_subprocess_path.length,
        &target->browser_subprocess_path, copy);
    target->multi_threaded_message_loop = src->multi_threaded_message_loop;
    target->windowless_rendering_enabled = src->windowless_rendering_enabled;
    target->command_line_args_disabled = src->command_line_args_disabled;

    cef_string_set(src->cache_path.str, src->cache_path.length,
        &target->cache_path, copy);
    cef_string_set(src->user_data_path.str, src->user_data_path.length,
        &target->user_data_path, copy);
    target->persist_session_cookies = src->persist_session_cookies;

    cef_string_set(src->user_agent.str, src->user_agent.length,
        &target->user_agent, copy);
    cef_string_set(src->product_version.str, src->product_version.length,
        &target->product_version, copy);
    cef_string_set(src->locale.str, src->locale.length, &target->locale, copy);

    cef_string_set(src->log_file.str, src->log_file.length, &target->log_file,
        copy);
    target->log_severity = src->log_severity;
    cef_string_set(src->javascript_flags.str, src->javascript_flags.length,
        &target->javascript_flags, copy);

    cef_string_set(src->resources_dir_path.str, src->resources_dir_path.length,
        &target->resources_dir_path, copy);
    cef_string_set(src->locales_dir_path.str, src->locales_dir_path.length,
        &target->locales_dir_path, copy);
    target->pack_loading_disabled = src->pack_loading_disabled;
    target->remote_debugging_port = src->remote_debugging_port;
    target->uncaught_exception_stack_size = src->uncaught_exception_stack_size;
    target->context_safety_implementation = src->context_safety_implementation;
    target->ignore_certificate_errors = src->ignore_certificate_errors;
    target->background_color = src->background_color;

    cef_string_set(src->accept_language_list.str,
        src->accept_language_list.length, &target->accept_language_list, copy);
  }
};

///
// Class representing initialization settings.
///
typedef CefStructBase<CefSettingsTraits> CefSettings;


struct CefRequestContextSettingsTraits {
  typedef cef_request_context_settings_t struct_type;

  static inline void init(struct_type* s) {
    s->size = sizeof(struct_type);
  }

  static inline void clear(struct_type* s) {
    cef_string_clear(&s->cache_path);
    cef_string_clear(&s->accept_language_list);
  }

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    cef_string_set(src->cache_path.str, src->cache_path.length,
        &target->cache_path, copy);
    target->persist_session_cookies = src->persist_session_cookies;
    target->ignore_certificate_errors = src->ignore_certificate_errors;
    cef_string_set(src->accept_language_list.str,
        src->accept_language_list.length, &target->accept_language_list, copy);
  }
};

///
// Class representing request context initialization settings.
///
typedef CefStructBase<CefRequestContextSettingsTraits>
    CefRequestContextSettings;


struct CefBrowserSettingsTraits {
  typedef cef_browser_settings_t struct_type;

  static inline void init(struct_type* s) {
    s->size = sizeof(struct_type);
  }

  static inline void clear(struct_type* s) {
    cef_string_clear(&s->standard_font_family);
    cef_string_clear(&s->fixed_font_family);
    cef_string_clear(&s->serif_font_family);
    cef_string_clear(&s->sans_serif_font_family);
    cef_string_clear(&s->cursive_font_family);
    cef_string_clear(&s->fantasy_font_family);
    cef_string_clear(&s->default_encoding);
    cef_string_clear(&s->accept_language_list);
  }

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    target->windowless_frame_rate = src->windowless_frame_rate;

    cef_string_set(src->standard_font_family.str,
        src->standard_font_family.length, &target->standard_font_family, copy);
    cef_string_set(src->fixed_font_family.str, src->fixed_font_family.length,
        &target->fixed_font_family, copy);
    cef_string_set(src->serif_font_family.str, src->serif_font_family.length,
        &target->serif_font_family, copy);
    cef_string_set(src->sans_serif_font_family.str,
        src->sans_serif_font_family.length, &target->sans_serif_font_family,
        copy);
    cef_string_set(src->cursive_font_family.str,
        src->cursive_font_family.length, &target->cursive_font_family, copy);
    cef_string_set(src->fantasy_font_family.str,
        src->fantasy_font_family.length, &target->fantasy_font_family, copy);

    target->default_font_size = src->default_font_size;
    target->default_fixed_font_size = src->default_fixed_font_size;
    target->minimum_font_size = src->minimum_font_size;
    target->minimum_logical_font_size = src->minimum_logical_font_size;

    cef_string_set(src->default_encoding.str, src->default_encoding.length,
        &target->default_encoding, copy);

    target->remote_fonts = src->remote_fonts;
    target->javascript = src->javascript;
    target->javascript_open_windows = src->javascript_open_windows;
    target->javascript_close_windows = src->javascript_close_windows;
    target->javascript_access_clipboard = src->javascript_access_clipboard;
    target->javascript_dom_paste = src->javascript_dom_paste;
    target->caret_browsing = src->caret_browsing;
    target->java = src->java;
    target->plugins = src->plugins;
    target->universal_access_from_file_urls =
        src->universal_access_from_file_urls;
    target->file_access_from_file_urls = src->file_access_from_file_urls;
    target->web_security = src->web_security;
    target->image_loading = src->image_loading;
    target->image_shrink_standalone_to_fit =
        src->image_shrink_standalone_to_fit;
    target->text_area_resize = src->text_area_resize;
    target->tab_to_links = src->tab_to_links;
    target->local_storage = src->local_storage;
    target->databases= src->databases;
    target->application_cache = src->application_cache;
    target->webgl = src->webgl;

    target->background_color = src->background_color;

    cef_string_set(src->accept_language_list.str,
        src->accept_language_list.length, &target->accept_language_list, copy);
  }
};

///
// Class representing browser initialization settings.
///
typedef CefStructBase<CefBrowserSettingsTraits> CefBrowserSettings;


struct CefURLPartsTraits {
  typedef cef_urlparts_t struct_type;

  static inline void init(struct_type* s) {}

  static inline void clear(struct_type* s) {
    cef_string_clear(&s->spec);
    cef_string_clear(&s->scheme);
    cef_string_clear(&s->username);
    cef_string_clear(&s->password);
    cef_string_clear(&s->host);
    cef_string_clear(&s->port);
    cef_string_clear(&s->origin);
    cef_string_clear(&s->path);
    cef_string_clear(&s->query);
  }

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    cef_string_set(src->spec.str, src->spec.length, &target->spec, copy);
    cef_string_set(src->scheme.str, src->scheme.length, &target->scheme, copy);
    cef_string_set(src->username.str, src->username.length, &target->username,
        copy);
    cef_string_set(src->password.str, src->password.length, &target->password,
        copy);
    cef_string_set(src->host.str, src->host.length, &target->host, copy);
    cef_string_set(src->port.str, src->port.length, &target->port, copy);
    cef_string_set(src->origin.str, src->origin.length, &target->origin, copy);
    cef_string_set(src->path.str, src->path.length, &target->path, copy);
    cef_string_set(src->query.str, src->query.length, &target->query, copy);
  }
};

///
// Class representing a URL's component parts.
///
typedef CefStructBase<CefURLPartsTraits> CefURLParts;


struct CefTimeTraits {
  typedef cef_time_t struct_type;

  static inline void init(struct_type* s) {}

  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    *target = *src;
  }
};

///
// Class representing a time.
///
class CefTime : public CefStructBase<CefTimeTraits> {
 public:
  typedef CefStructBase<CefTimeTraits> parent;

  CefTime() : parent() {}
  CefTime(const cef_time_t& r) : parent(r) {}  // NOLINT(runtime/explicit)
  CefTime(const CefTime& r) : parent(r) {}  // NOLINT(runtime/explicit)
  explicit CefTime(time_t r) : parent() { SetTimeT(r); }
  explicit CefTime(double r) : parent() { SetDoubleT(r); }

  // Converts to/from time_t.
  void SetTimeT(time_t r) {
    cef_time_from_timet(r, this);
  }
  time_t GetTimeT() const {
    time_t time = 0;
    cef_time_to_timet(this, &time);
    return time;
  }

  // Converts to/from a double which is the number of seconds since epoch
  // (Jan 1, 1970). Webkit uses this format to represent time. A value of 0
  // means "not initialized".
  void SetDoubleT(double r) {
    cef_time_from_doublet(r, this);
  }
  double GetDoubleT() const {
    double time = 0;
    cef_time_to_doublet(this, &time);
    return time;
  }

  // Set this object to now.
  void Now() {
    cef_time_now(this);
  }

  // Return the delta between this object and |other| in milliseconds.
  long long Delta(const CefTime& other) {
    long long delta = 0;
    cef_time_delta(this, &other, &delta);
    return delta;
  }
};


struct CefCookieTraits {
  typedef cef_cookie_t struct_type;

  static inline void init(struct_type* s) {}

  static inline void clear(struct_type* s) {
    cef_string_clear(&s->name);
    cef_string_clear(&s->value);
    cef_string_clear(&s->domain);
    cef_string_clear(&s->path);
  }

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    cef_string_set(src->name.str, src->name.length, &target->name, copy);
    cef_string_set(src->value.str, src->value.length, &target->value, copy);
    cef_string_set(src->domain.str, src->domain.length, &target->domain, copy);
    cef_string_set(src->path.str, src->path.length, &target->path, copy);
    target->secure = src->secure;
    target->httponly = src->httponly;
    target->creation = src->creation;
    target->last_access = src->last_access;
    target->has_expires = src->has_expires;
    target->expires = src->expires;
  }
};

///
// Class representing a cookie.
///
typedef CefStructBase<CefCookieTraits> CefCookie;


struct CefGeopositionTraits {
  typedef cef_geoposition_t struct_type;

  static inline void init(struct_type* s) {}

  static inline void clear(struct_type* s) {
    cef_string_clear(&s->error_message);
  }

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    target->latitude = src->latitude;
    target->longitude = src->longitude;
    target->altitude = src->altitude;
    target->accuracy = src->accuracy;
    target->altitude_accuracy = src->altitude_accuracy;
    target->heading = src->heading;
    target->speed = src->speed;
    target->timestamp = src->timestamp;
    target->error_code = src->error_code;
    cef_string_set(src->error_message.str, src->error_message.length,
        &target->error_message, copy);
  }
};

///
// Class representing a geoposition.
///
typedef CefStructBase<CefGeopositionTraits> CefGeoposition;


struct CefPageRangeTraits {
  typedef cef_page_range_t struct_type;

  static inline void init(struct_type* s) {}
  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    *target = *src;
  }
};

///
// Class representing a print job page range.
///
class CefPageRange : public CefStructBase<CefPageRangeTraits> {
 public:
  typedef CefStructBase<CefPageRangeTraits> parent;

  CefPageRange() : parent() {}
  CefPageRange(const cef_page_range_t& r)  // NOLINT(runtime/explicit)
      : parent(r) {}
  CefPageRange(const CefPageRange& r)  // NOLINT(runtime/explicit)
      : parent(r) {} 
  CefPageRange(int from, int to) : parent() {
    Set(from, to);
  }

  void Set(int from, int to) {
    this->from = from, this->to = to;
  }
};

inline bool operator==(const CefPageRange& a, const CefPageRange& b) {
  return a.from == b.from && a.to == b.to;
}

inline bool operator!=(const CefPageRange& a, const CefPageRange& b) {
  return !(a == b);
}


struct CefCursorInfoTraits {
  typedef cef_cursor_info_t struct_type;

  static inline void init(struct_type* s) {}

  static inline void clear(struct_type* s) {}

  static inline void set(const struct_type* src, struct_type* target,
      bool copy) {
    target->hotspot = src->hotspot;
    target->image_scale_factor = src->image_scale_factor;
    target->buffer = src->buffer;
    target->size = src->size;
  }
};

///
// Class representing cursor information.
///
typedef CefStructBase<CefCursorInfoTraits> CefCursorInfo;

#endif  // CEF_INCLUDE_INTERNAL_CEF_TYPES_WRAPPERS_H_
