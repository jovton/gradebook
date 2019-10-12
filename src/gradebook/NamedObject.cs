using System;

namespace GradeBook
{
    public class NamedObject
    {
        private string name;
        
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                }
                else
                {
                    throw new InvalidOperationException($"Cannot set '{nameof(Name)}' property of '{nameof(InMemoryBook)}' to an empty value.");
                }
            }
        }
    }
}
