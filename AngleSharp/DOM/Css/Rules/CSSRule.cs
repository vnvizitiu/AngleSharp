﻿namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS rule.
    /// </summary>
    abstract class CSSRule : ICssRule
    {
        #region Fields

        readonly CssRuleType _type;
        ICssStyleSheet _ownerSheet;
        ICssRule _parentRule;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS rule.
        /// </summary>
        internal CSSRule(CssRuleType type)
        {
            _type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the textual representation of the rule.
        /// </summary>
        public String CssText
        {
            get { return ToCss(); }
            set
            {
                var rule = CssParser.ParseRule(value);

                if (rule == null)
                    throw new DomException(ErrorCode.Syntax);
                else if (rule.Type != _type)
                    throw new DomException(ErrorCode.InvalidModification);

                ReplaceWith(rule);
            }
        }

        /// <summary>
        /// Gets the containing rule, otherwise null.
        /// </summary>
        public ICssRule Parent
        {
            get { return _parentRule; }
            internal set { _parentRule = value; }
        }

        /// <summary>
        /// Gets the CSSStyleSheet object for the style sheet that contains this rule.
        /// </summary>
        public ICssStyleSheet Owner
        {
            get { return _ownerSheet; }
            internal set { _ownerSheet = value; }
        }


        /// <summary>
        /// Gets the type constant indicating the type of CSS rule.
        /// </summary>
        public CssRuleType Type
        {
            get { return _type; }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Replaces the current object with the given rule.
        /// The types are equal.
        /// </summary>
        /// <param name="rule">The new rule.</param>
        protected abstract void ReplaceWith(ICssRule rule);

        /// <summary>
        /// Computes the style for the given element within the specified window
        /// context. Writes the properties into the specified style declaration.
        /// </summary>
        /// <param name="style">The declaration that is used.</param>
        /// <param name="device">The given render device.</param>
        /// <param name="element">The element that is computed.</param>
        internal virtual void ComputeStyle(PropertyBag style, RenderDevice device, IElement element)
        {
            //By default nothing gets computed.
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected abstract String ToCss();

        #endregion
    }
}
