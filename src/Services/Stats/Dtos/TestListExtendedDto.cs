using System;
using System.Collections.Generic;
using System.Linq;
using HealthPanel.Core.Entities;
using Microsoft.OpenApi.Extensions;

namespace HealthPanel.Services.Stats.Dtos
{
    public enum TestListType
    {
        MedTest = 1,
        LabTest = 2,
        Examination = 3,
        Consultation = 4,
        TestPanel = 5,
        LabTestPanel = 6
    }

    public class TestListItemDto : IDto
    {
        public int Id { get; set; }
        public TestListType Type { get; set; }
        public int Index { get; set; }
        public object Item { get; set; }
        public TimeSpan TTL { get; set; }

        public TestListItemDto() { }
        public TestListItemDto(int id, int index,
            TestListType type, object item, TimeSpan ttl)
        {
            this.Id = id;
            this.Index = index;
            this.Type = type;
            this.Item = item;
            this.TTL = ttl;
        }
    }

    public class TestListExtendedDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SortedDictionary<string, TestListItemDto> Index { get; internal set; }

        public TestListExtendedDto() { }
        public TestListExtendedDto(TestList entity) : this(entity.Id, entity.Name) { }
        public TestListExtendedDto(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Index = new SortedDictionary<string, TestListItemDto>();
        }

        public TestListExtendedDto Add(TestListItemDto item)
        {
            string index = string.Format("{0}_{1}{2}",
                item.Index.ToString("0000"),
                item.Type.GetDisplayName(),
                (item.Item as IDto).Id
            );
            this.Index.Add(index, item);

            return this;
        }

        public TestListExtendedDto Add(IEnumerable<TestListItemDto> items)
        {
            items.ToList()?.ForEach(p => this.Add(p));

            return this;
        }

        public void Add(TestListType type, IDto item,
            TimeSpan ttl, int index = 0)
        {
            var itemI = string.Format("{0}_{1}{2}",
                index.ToString("0000"),
                type.GetDisplayName(),
                item.Id);

            switch (type)
            {
                case TestListType.MedTest:
                    this.Index.Add(itemI, new TestListItemDto()
                    { Item = item as MedTestDto, Type = type, Index = index, TTL = ttl });
                    break;
                case TestListType.LabTest:
                    this.Index.Add(itemI, new TestListItemDto()
                    { Item = item as LabTestDto, Type = type, Index = index, TTL = ttl });
                    break;
                case TestListType.Examination:
                    this.Index.Add(itemI, new TestListItemDto()
                    { Item = item as ExaminationDto, Type = type, Index = index, TTL = ttl });
                    break;
                case TestListType.Consultation:
                    this.Index.Add(itemI, new TestListItemDto()
                    { Item = item as ConsultationDto, Type = type, Index = index, TTL = ttl });
                    break;
                case TestListType.TestPanel:
                    this.Index.Add(itemI, new TestListItemDto()
                    { Item = item as TestPanelDto, Type = type, Index = index, TTL = ttl });
                    break;
                case TestListType.LabTestPanel:
                    this.Index.Add(itemI, new TestListItemDto()
                    { Item = item as LabTestPanelDto, Type = type, Index = index, TTL = ttl });
                    break;

                default:
                    throw new Exception("Wrong type");
            }
        }

        public void Add(MedTestDto item, TimeSpan ttl, int index = 0)
            => this.Add(TestListType.MedTest, item, ttl, index);
        public void Add(LabTestDto item, TimeSpan ttl, int index = 0)
            => this.Add(TestListType.LabTest, item, ttl, index);
        public void Add(ExaminationDto item, TimeSpan ttl, int index = 0)
            => this.Add(TestListType.Examination, item, ttl, index);
        public void Add(ConsultationDto item, TimeSpan ttl, int index = 0)
            => this.Add(TestListType.Consultation, item, ttl, index);
        public void Add(TestPanelDto item, TimeSpan ttl, int index = 0)
            => this.Add(TestListType.TestPanel, item, ttl, index);
        public void Add(LabTestPanelDto item, TimeSpan ttl, int index = 0)
            => this.Add(TestListType.LabTestPanel, item, ttl, index);

    }
}