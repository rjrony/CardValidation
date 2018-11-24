namespace CardValidation.Repository
{
    public class DefaultData
    {
        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Seed(CardValidationContext context)
        {
            context.SaveChanges();
        }
    }
}
