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
        TestPanel = 4,
        LabTestPanel = 5
    }

    public class TestListItemDto : IDto
    {
        public int Id { get; set; }
        public TestListType Type { get; set; }
        public int Index { get; set; }
        public object Item { get; set; }

        public TestListItemDto() { }
        public TestListItemDto(int id, int index, TestListType type, object item)
        {
            this.Id = id;
            this.Index = index;
            this.Type = type;
            this.Item = item;
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

        public void Add(TestListType type, IDto item, int index = 0)
        {
            var itemI = $"{index}_{type.GetDisplayName()}{item.Id}";

            switch (type)
            {
                case TestListType.MedTest:
                    this.Index.Add(itemI,
                        new TestListItemDto() { Item = item as MedTestDto, Type = type, Index = index });
                    break;
                case TestListType.LabTest:
                    this.Index.Add(itemI,
                        new TestListItemDto() { Item = item as LabTestDto, Type = type, Index = index });
                    break;
                case TestListType.Examination:
                    this.Index.Add(itemI,
                        new TestListItemDto() { Item = item as ExaminationDto, Type = type, Index = index });
                    break;
                case TestListType.TestPanel:
                    this.Index.Add(itemI,
                        new TestListItemDto() { Item = item as TestPanelDto, Type = type, Index = index });
                    break;
                case TestListType.LabTestPanel:
                    this.Index.Add(itemI,
                        new TestListItemDto() { Item = item as LabTestPanelDto, Type = type, Index = index });
                    break;

                default:
                    throw new Exception("Wrong type");
            }
        }

        public void Add(MedTestDto item, int index = 0) => this.Add(TestListType.MedTest, item, index);
        public void Add(LabTestDto item, int index = 0) => this.Add(TestListType.LabTest, item, index);
        public void Add(ExaminationDto item, int index = 0) => this.Add(TestListType.Examination, item, index);
        public void Add(TestPanelDto item, int index = 0) => this.Add(TestListType.TestPanel, item, index);
        public void Add(LabTestPanelDto item, int index = 0) => this.Add(TestListType.LabTestPanel, item, index);

    }
}