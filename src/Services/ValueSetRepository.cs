using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Cdc.Vocabulary.Entities;

namespace Cdc.Vocabulary.Services
{
    public sealed class ValueSetRepository : IValueSetRepository
    {
        private ValueSetContext _context;

        public ValueSetRepository(ValueSetContext context)
        {
            _context = context;
        }

        public IEnumerable<ValueSet> GetValueSets()
        {
            return _context.ValueSets.OrderBy(a => a.Code);
        }

        public IEnumerable<ValueSet> GetValueSets(IEnumerable<Guid> ids)
        {
            return _context.ValueSets.Where(a => ids.Contains(a.Id))
                .OrderBy(a => a.Code)
                .ToList();
        }

        public ValueSet GetValueSet(Guid id)
        {
            return _context.ValueSets.FirstOrDefault(a => a.Id == id);
        }

        public void AddValueSet(ValueSet valueSet)
        {
            valueSet.Id = Guid.NewGuid();
            valueSet.ValueSetCreatedDate = DateTime.Now;
            valueSet.ValueSetLastRevisionDate = DateTime.Now;
            valueSet.StatusDate = DateTime.Now;
            _context.ValueSets.Add(valueSet);
        }

        public void DeleteValueSet(ValueSet valueSet)
        {
            _context.ValueSets.Remove(valueSet);
        }

        public void UpdateValueSet(ValueSet valueSet)
        {
            // no code in this implementation
            valueSet.ValueSetLastRevisionDate = DateTime.Now;
        }

        public bool ValueSetExists(Guid id)
        {
            return _context.ValueSets.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}