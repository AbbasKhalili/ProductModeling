using System;
using System.Collections.Generic;
using System.Linq;
using ProductManagement.Domain.Model.Constraints;

namespace ProductManagement.Domain.Model.Product.ProductConstraints
{
    public class SelectiveConstraint : ProductConstraint
    {
        private List<Option> options;
        public IReadOnlyList<Option> Options => options.AsReadOnly();

        public SelectiveConstraint(Constraint constraint, List<Option> options)
            : base(constraint)
        {
            GuardAgaintsDuplicateValueIn(options);

            this.options = options;
        }

        private static void GuardAgaintsDuplicateValueIn(List<Option> options)
        {
            var hasDuplicateValue = options
                .GroupBy(product => product.Value, 
                                    (key, value) => new {key, Count = value.Count()})
                .Any(group => group.Count > 1);

            if (hasDuplicateValue)
                throw new DuplicateOptionException();
        }
    }
}