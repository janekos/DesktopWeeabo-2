﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopWeeabo2.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DesktopWeeabo2.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to CREATE TABLE &apos;anime_entries&apos; (
        ///                    &apos;id&apos; integer not null primary key,
        ///                    &apos;mean_score&apos; integer default 0,
        ///                    &apos;episodes&apos; integer default 0,
        ///                    &apos;duration&apos; integer default 0,
        ///                    &apos;type&apos; string default &apos;&apos;,
        ///                    &apos;format&apos; string default &apos;&apos;,
        ///                    &apos;status&apos; string default &apos;&apos;,
        ///                    &apos;description&apos; string default &apos;&apos;,
        ///                    &apos;genres&apos; string default &apos;&apos;,
        ///                    &apos; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateAnimeTable {
            get {
                return ResourceManager.GetString("CreateAnimeTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE &apos;manga_entries&apos; (
        ///                    &apos;id&apos; integer not null primary key,
        ///                    &apos;mean_score&apos; integer default 0,
        ///                    &apos;volumes&apos; integer default 0,
        ///                    &apos;chapters&apos; integer default 0,
        ///                    &apos;type&apos; string default &apos;&apos;,
        ///                    &apos;format&apos; string default &apos;&apos;,
        ///                    &apos;status&apos; string default &apos;&apos;,
        ///                    &apos;description&apos; string default &apos;&apos;,
        ///                    &apos;genres&apos; string default &apos;&apos;,
        ///                    &apos;s [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateMangaTable {
            get {
                return ResourceManager.GetString("CreateMangaTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to query ($id: Int, $page: Int, $search: String, $sort: [MediaSort]) {
        ///	Page(page: $page, perPage: 50) {
        ///		pageInfo { 
        ///			total
        ///			hasNextPage
        ///		}
        ///		media(id: $id, type: ANIME, search: $search, sort: $sort) {
        ///			id 
        ///			title { 
        ///				english 
        ///				romaji
        ///				native
        ///			}
        ///			synonyms 
        ///			genres
        ///			type 
        ///			meanScore
        ///			format 
        ///			status 
        ///			description(asHtml: false) 
        ///			startDate{
        ///				year
        ///				month
        ///				day
        ///			}
        ///			endDate{
        ///				year
        ///				month
        ///				day
        ///			}
        ///			episodes 
        ///			duration 
        ///	 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SearchAnimeQuery {
            get {
                return ResourceManager.GetString("SearchAnimeQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to query ($id: Int, $page: Int, $search: String, $sort: [MediaSort] ) {
        ///	Page(page: $page, perPage: 50) {
        ///		pageInfo { 
        ///			total
        ///			hasNextPage
        ///		}
        ///		media(id: $id, type: MANGA, search: $search, sort: $sort) {
        ///			id 
        ///			title { 
        ///				english 
        ///				romaji
        ///				native
        ///			}
        ///			synonyms 
        ///			genres 
        ///			meanScore
        ///			type
        ///			format 
        ///			status 
        ///			description(asHtml: false) 
        ///			volumes
        ///			chapters
        ///			coverImage{ large }
        ///		}
        ///	}
        ///}.
        /// </summary>
        internal static string SearchMangaQuery {
            get {
                return ResourceManager.GetString("SearchMangaQuery", resourceCulture);
            }
        }
    }
}
