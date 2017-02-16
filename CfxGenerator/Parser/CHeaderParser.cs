﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser {

    internal abstract class CHeaderParser : Parser {

        // rules for both c header parsers

        protected bool ParseCefExportFunction(List<FunctionData> functions) {
            Mark();
            var f = new FunctionData();
            ParseSummary(f.Comments);
            var success =
                Skip(@"CEF_EXPORT\b");
            if(success) {
                Scan(@"const\b", () => f.Signature.ReturnValueIsConst = true);
                Ensure(
                    ParseType(f.Signature.ReturnType)
                    && Scan(@"\w+", () => f.Name = Value)
                    && Skip(@"\(")
                    && ParseParameterList(f.Signature.Arguments)
                    && Skip(@"\)\s*;")
                );
            }
            if(success)
                functions.Add(f);
            Unmark(success);
            return success;
        }

        protected bool ParseParameterList(List<ParameterData> parameters) {
            var p = new ParameterData();
            while(ParseParameter(p)) {
                parameters.Add(p);
                if(!Skip(",")) break;
                p = new ParameterData();
            }
            return true;
        }

        protected bool ParseParameter(ParameterData parameter) {
            Mark();
            Scan(@"const\b", () => parameter.IsConst = true);
            var success =
                ParseType(parameter.ParameterType)
                && Scan(@"\w+", () => parameter.Var = Value);
            Unmark(success);
            return success;
        }

        protected bool ParseType(TypeData type) {
            Mark();
            var success =
                Scan("long long", () => type.Name = Value)
                || Scan(@"unsigned \w+", () => type.Name = Value)
                || Scan(@"(?:struct _)?(\w+)", () => type.Name = Group01);
            if(success) {
                var tmp =
                    Scan(@"\*\s*const\s*\*", () => type.Indirection = Value)
                    || Scan(@"const\s*\*", () => type.Indirection = Value)
                    || Scan(@"(?:\s*\*)+", () => type.Indirection = Value);
            }
            Unmark(success);
            return success;
        }
    }
}
