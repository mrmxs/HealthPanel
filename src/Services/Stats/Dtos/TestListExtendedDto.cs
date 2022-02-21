using System;
using System.Collections.Generic;
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

    public class TestListItem
    {
        public TestListType Type { get; set; }
        public object Item { get; set; }
    }

    public class TestListExtendedDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SortedDictionary<string, TestListItem> Index { get; internal set; }

        public TestListExtendedDto() { }
        public TestListExtendedDto(TestList entity) : this(entity.Id, entity.Name) { }
        public TestListExtendedDto(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Index = new SortedDictionary<string, TestListItem>();
        }

        public void Add(TestListType type, IDto item, int index = 0)
        {
            var itemI = $"{index}_{type.GetDisplayName()}{item.Id}";

            switch (type)
            {
                case TestListType.MedTest:
                    this.Index.Add(itemI,
                        new TestListItem() { Type = type, Item = item as MedTestDto });
                    break;
                case TestListType.LabTest:
                    this.Index.Add(itemI,
                        new TestListItem() { Type = type, Item = item as LabTestDto });
                    break;
                case TestListType.Examination:
                    this.Index.Add(itemI,
                        new TestListItem() { Type = type, Item = item as ExaminationDto });
                    break;
                case TestListType.TestPanel:
                    this.Index.Add(itemI,
                        new TestListItem() { Type = type, Item = item as TestPanelDto });
                    break;
                case TestListType.LabTestPanel:
                    this.Index.Add(itemI,
                        new TestListItem() { Type = type, Item = item as LabTestPanelDto });
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