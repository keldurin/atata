﻿namespace Atata
{
    /// <summary>
    /// Represents the image control (&lt;img&gt;).
    /// Default search finds the first occurring &lt;img&gt; element.
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner page object.</typeparam>
    [ControlDefinition("img")]
    public class Image<TOwner> : Control<TOwner>
        where TOwner : PageObject<TOwner>
    {
        /// <summary>
        /// Gets the <see cref="DataProvider{TData, TOwner}"/> instance for the <c>src</c> attribute.
        /// </summary>
        public DataProvider<string, TOwner> Source => Attributes.Src;

        /// <summary>
        /// Gets the <see cref="DataProvider{TData, TOwner}"/> instance for the value indicating whether the image file is loaded.
        /// </summary>
        public DataProvider<bool, TOwner> IsLoaded => GetOrCreateDataProvider("loaded state", GetIsLoaded);

        protected virtual bool GetIsLoaded()
        {
            return (bool)Driver.ExecuteScript("return arguments[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && arguments[0].naturalWidth > 0", Scope);
        }
    }
}
