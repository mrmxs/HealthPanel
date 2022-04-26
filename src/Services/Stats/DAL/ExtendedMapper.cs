using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Services.Stats.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HealthPanel.Services.Stats.DAL
{
    public partial class Mapper : IMapper
    {
        #region Extended

        public IEnumerable<TestListItemDto> Map<T, D>(
            TestListType type, IEnumerable<TestToTestList> tttls)
            where T : IEnumerable<TestToTestList>
            where D : IEnumerable<TestListItemDto>
        {
            int id = 0;

            return tttls.Where(p =>
            {
                // Find all ids of given type
                id = type switch
                {
                    TestListType.MedTest => p.MedTestId,
                    TestListType.LabTest => p.LabTestId,
                    TestListType.Examination => p.ExaminationId,
                    TestListType.Consultation => p.ConsultationId,
                    TestListType.TestPanel => p.TestPanelId,
                    TestListType.LabTestPanel => p.LabTestPanelId,
                    _ => throw new Exception(),
                };

                return id != 0;
            })
            .Select(async p =>
            {
                // Get entity of given type by id
                IEntity entity = type switch
                {
                    TestListType.MedTest => await _sugar.Tests.Id(id),
                    TestListType.LabTest => await _sugar.LTests.Id(id),
                    TestListType.Examination => await _sugar.Exams.Id(id),
                    TestListType.Consultation => await _sugar.Cons.Id(id),
                    TestListType.TestPanel => await _sugar.TPs.Id(id),
                    TestListType.LabTestPanel => await _sugar.LTPs.Id(id),
                    _ => throw new Exception(),
                };

                // Map Entity to TestListItemDto
                return new TestListItemDto(
                    id: p.Id,
                    index: p.Index,
                    type: type,
                    item: await this.Map(entity),
                    ttl: p.TTL
                );
            }).Select(t => t.Result);
        }

        public async Task<TestListExtendedDto> Map<T>(TestList entity)
            where T : TestListExtendedDto
        {
            var tttls = new List<TestToTestList>(
               await _context.TestsToTestList.ToListAsync()
           ).Where(p => p.TestListId == entity.Id);

            // Get DTOs to all items and add them to the TestList Index
            return new TestListExtendedDto(entity)
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.MedTest, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.LabTest, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.Examination, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.Consultation, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.TestPanel, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.LabTestPanel, tttls));
        }

        #endregion
    }
}