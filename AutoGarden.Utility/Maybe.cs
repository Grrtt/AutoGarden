namespace AutoGarden.Utility
{
    using System;

    public class Maybe<T>
    {
        private readonly bool hasItem;

        private readonly T value;

        public Maybe()
        {
            hasItem = false;
        }

        public Maybe(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.value = value;
            hasItem = true;
        }

        public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
        {
            if (nothing == null)
            {
                throw new ArgumentNullException(nameof(nothing));
            }

            if (just == null)
            {
                throw new ArgumentNullException(nameof(just));
            }

            return hasItem ? just(value) : nothing;
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (!hasItem)
            {
                return new Maybe<TResult>();
            }

            return new Maybe<TResult>(selector(value));
        }

        public Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (!hasItem)
            {
                return new Maybe<TResult>();
            }

            return selector(value);
        }
    }
}