using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nano.Entities
{
	class DNACollection : IEnumerable<DNA>
	{
		public List<SkillDNA> Skills { get; private set; }
		public List<AimSkillDNA> AimSkills { get; private set; }
		public List<AreaSkillDNA> AreaSkills { get; private set; }
		public List<DNA> DNA { get; private set; }
		public DNACollection()
		{

		}

		public void Add(DNA dna)
		{
			DNA.Add(dna);
			if (dna is SkillDNA) {
				Skills.Add((SkillDNA)dna);
				if (dna is AimSkillDNA) {
					AimSkills.Add((AimSkillDNA)dna);
				}
				if (dna is AreaSkillDNA) {
					AreaSkills.Add((AreaSkillDNA)dna);
				}
			}
		}

		static Random random = new Random();
		public static DNACollection RandomBlend(DNACollection a, DNACollection b)
		{
			var result = new DNACollection();
			foreach (var dna in a) {
				if (random.NextDouble() > 0.5) {
					result.Add(dna);
				}
			}
			foreach (var dna in b) {
				if (random.NextDouble() > 0.5) {
					result.Add(dna);
				}
			}
			return result;
		}

		public IEnumerator<DNA> GetEnumerator()
		{
			return DNA.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return DNA.GetEnumerator();
		}
	}
}
