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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
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
        public static global::System.Globalization.CultureInfo Culture {
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
        ///			startDate{
        ///				year
        ///	 [rest of string was truncated]&quot;;.
        /// </summary>
        public static string AnilistSearchQuery {
            get {
                return ResourceManager.GetString("AnilistSearchQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE &apos;anime_entries&apos; (
        ///	&apos;id&apos; integer not null primary key,
        ///	&apos;id_mal&apos; integer,
        ///	&apos;average_score&apos; integer,
        ///	&apos;episodes&apos; integer,
        ///	&apos;duration&apos; integer,
        ///	&apos;type&apos; string,
        ///	&apos;format&apos; string,
        ///	&apos;status&apos; string,
        ///	&apos;description&apos; string,
        ///	&apos;genres&apos; string,
        ///	&apos;synonyms&apos; string,
        ///	&apos;title_english&apos; string,
        ///	&apos;title_romaji&apos; string,
        ///	&apos;title_native&apos; string,
        ///	&apos;start_date&apos; text,
        ///	&apos;end_date&apos; text,
        ///	&apos;cover_image&apos; string,
        ///	&apos;viewing_status&apos; string,
        ///	&apos;personal_score&apos; integer,
        ///	&apos;personal_review&apos; string,
        ///	&apos;watch_pr [rest of string was truncated]&quot;;.
        /// </summary>
        public static string CreateAnimeTable {
            get {
                return ResourceManager.GetString("CreateAnimeTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE &apos;manga_entries&apos; (
        ///	&apos;id&apos; integer not null primary key,
        ///	&apos;id_mal&apos; integer,
        ///	&apos;average_score&apos; integer,
        ///	&apos;volumes&apos; integer,
        ///	&apos;chapters&apos; integer,
        ///	&apos;type&apos; string,
        ///	&apos;format&apos; string,
        ///	&apos;status&apos; string,
        ///	&apos;description&apos; string,
        ///	&apos;genres&apos; string,
        ///	&apos;synonyms&apos; string,
        ///	&apos;title_english&apos; string,
        ///	&apos;title_romaji&apos; string,
        ///	&apos;title_native&apos; string,
        ///	&apos;cover_image&apos; string,
        ///	&apos;reading_status&apos; string,
        ///	&apos;personal_score&apos; integer,
        ///	&apos;personal_review&apos; string,
        ///	&apos;read_priority&apos; integer,
        ///	&apos;reread_count&apos; integer, [rest of string was truncated]&quot;;.
        /// </summary>
        public static string CreateMangaTable {
            get {
                return ResourceManager.GetString("CreateMangaTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This program doesn&apos;t collect or send any data. Only input/output type operations it does is:
        ///    1. queries the Anilist database, when you do online searches;
        ///    2. adds a new folder with a configuration and a database file in Documents folder;
        ///    3. optionally backs up the database;
        ///
        ///You can review this program&apos;s code at &quot;https://github.com/janekos/DesktopWeeabo-2&quot;.
        ///
        ///If you got this program from somewhere other than this page &quot;https://github.com/janekos/DesktopWeeabo-2/releases&quot; then I strongly su [rest of string was truncated]&quot;;.
        /// </summary>
        public static string DisclaimerMessage {
            get {
                return ResourceManager.GetString("DisclaimerMessage", resourceCulture);
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
        public static string GetAnimeByMALIds {
            get {
                return ResourceManager.GetString("GetAnimeByMALIds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap load {
            get {
                object obj = ResourceManager.GetObject("load", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
