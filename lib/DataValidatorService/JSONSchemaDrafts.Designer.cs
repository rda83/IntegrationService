﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegrationService.DataValidatorService {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class JSONSchemaDrafts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal JSONSchemaDrafts() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IntegrationService.DataValidatorService.JSONSchemaDrafts", typeof(JSONSchemaDrafts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на {
        ///    &quot;id&quot;: &quot;http://json-schema.org/draft-04/schema#&quot;,
        ///    &quot;$schema&quot;: &quot;http://json-schema.org/draft-04/schema#&quot;,
        ///    &quot;description&quot;: &quot;Core schema meta-schema&quot;,
        ///    &quot;definitions&quot;: {
        ///        &quot;schemaArray&quot;: {
        ///            &quot;type&quot;: &quot;array&quot;,
        ///            &quot;minItems&quot;: 1,
        ///            &quot;items&quot;: { &quot;$ref&quot;: &quot;#&quot; }
        ///        },
        ///        &quot;positiveInteger&quot;: {
        ///            &quot;type&quot;: &quot;integer&quot;,
        ///            &quot;minimum&quot;: 0
        ///        },
        ///        &quot;positiveIntegerDefault0&quot;: {
        ///            &quot;allOf&quot;: [ { &quot;$ref&quot;: &quot;#/definitions/positiveInteger&quot; }, {  [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string Draft4 {
            get {
                return ResourceManager.GetString("Draft4", resourceCulture);
            }
        }
    }
}