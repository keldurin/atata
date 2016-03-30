﻿using Humanizer;
using System;

namespace Atata
{
    public abstract class UIComponent<TOwner> : UIComponent
        where TOwner : PageObject<TOwner>
    {
        protected UIComponent()
        {
        }

        protected internal new TOwner Owner
        {
            get { return (TOwner)base.Owner; }
            internal set { base.Owner = value; }
        }

        protected internal new UIComponent<TOwner> Parent
        {
            get { return (UIComponent<TOwner>)base.Parent; }
            internal set { base.Parent = value; }
        }

        protected internal virtual void InitComponent()
        {
            UIComponentResolver.Resolve<TOwner>(this);
        }

        public new TOwner VerifyExists()
        {
            base.VerifyExists();
            return Owner;
        }

        public new TOwner VerifyMissing()
        {
            base.VerifyMissing();
            return Owner;
        }

        public TOwner VerifyContent(string content, TermMatch match = TermMatch.Equals)
        {
            string matchActionText = match.ToSentenceString();
            Log.StartVerificationSection("{0} component text {1} '{2}'", ComponentName, matchActionText, content);

            var matchPredicate = match.GetPredicate();
            string actualText = Scope.Text;
            bool doesMatch = matchPredicate(actualText, content);
            string errorMessage = ExceptionFactory.BuildAssertionErrorMessage(
                "String that {0} '{1}'".FormatWith(matchActionText, content),
                actualText,
                "{0} component text doesn't match criteria", ComponentName);
            Assert.That(doesMatch, errorMessage);

            Log.EndSection();
            return Owner;
        }

        protected TComponent CreateComponent<TComponent>(string name, params Attribute[] attributes)
            where TComponent : UIComponent<TOwner>
        {
            return UIComponentResolver.CreateComponent<TComponent, TOwner>(this, name, attributes);
        }
    }
}
