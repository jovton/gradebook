using System;

namespace GradeBook
{
    public class NamedObject : INamedObject
    {
        private string _name;
        
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _name = value;
                }
                else
                {
                    throw new InvalidOperationException($"Cannot set '{nameof(Name)}' property of '{nameof(InMemoryBook)}' to an empty value.");
                }
            }
        }
    }
}
