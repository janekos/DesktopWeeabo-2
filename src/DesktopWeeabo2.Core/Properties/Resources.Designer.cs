﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopWeeabo2.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DesktopWeeabo2.Core.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to query ($id: Int, $page: Int, $search: String, $sort: [MediaSort], $genres: [String], $isAdult: Boolean) {
        ///	Page(page: $page, perPage: 50) {
        ///		pageInfo {
        ///			total
        ///			lastPage
        ///			hasNextPage
        ///		}
        ///		media(id: $id, type: {mediaTypeToReplace}, search: $search, sort: $sort, genre_in: $genres, isAdult: $isAdult) {
        ///			id
        ///			idMal
        ///			title { 
        ///				english 
        ///				romaji
        ///				native
        ///			}
        ///			synonyms 
        ///			genres
        ///			type 
        ///			averageScore
        ///			format 
        ///			status 
        ///			description(asHtml: false) 
        ///			startDate{        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AnilistSearchQuery {
            get {
                return ResourceManager.GetString("AnilistSearchQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to query ($malIds: [Int], $page: Int) {
        ///	Page(page: $page, perPage: 50) {
        ///		pageInfo { 
        ///			total
        ///			hasNextPage
        ///		}
        ///		media(idMal_in: $malIds, type: ANIME) {
        ///			id
        ///			idMal
        ///			title { 
        ///				english 
        ///				romaji
        ///				native
        ///			}
        ///			synonyms 
        ///			genres
        ///			type 
        ///			averageScore
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
        ///			volumes
        ///			chapters
        ///			coverImage{ l [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetAnimeByMALIds {
            get {
                return ResourceManager.GetString("GetAnimeByMALIds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to query ($ids: [Int], $page: Int) {
        ///	Page(page: $page, perPage: 50) {
        ///		pageInfo { 
        ///			total
        ///			hasNextPage
        ///		}
        ///		media(id_in: $ids, type: {typeToQuery}) {
        ///			id
        ///			idMal
        ///			title { 
        ///				english 
        ///				romaji
        ///				native
        ///			}
        ///			synonyms 
        ///			genres
        ///			type 
        ///			averageScore
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
        ///			volumes
        ///			chapters
        ///			coverImage{ la [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetEntriesByIds {
            get {
                return ResourceManager.GetString("GetEntriesByIds", resourceCulture);
            }
        }
    }
}
