using System;
using System.Collections.Generic;
using System.Linq;
using HealthPanel.Core.Entities;
using Microsoft.AspNetCore.Http;

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

    public class TestListExtendedDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MedTestDto> MedTests { get; internal set; }
        public IEnumerable<LabTestDto> LabTests { get; internal set; }
        public IEnumerable<ExaminationDto> Examinations { get; internal set; }
        public IEnumerable<TestPanelDto> TestPanels { get; internal set; }
        public IEnumerable<LabTestPanelDto> LabTestPanels { get; internal set; }

        public TestListExtendedDto() { }
        public TestListExtendedDto(TestList entity) : this(entity.Id, entity.Name) { }
        public TestListExtendedDto(int id, string name)
        {
            this.Id = id;
            this.Name = name;

            this.MedTests = new List<MedTestDto>();
            this.LabTests = new List<LabTestDto>();
            this.Examinations = new List<ExaminationDto>();
            this.TestPanels = new List<TestPanelDto>();
            this.LabTestPanels = new List<LabTestPanelDto>();
        }

        public TestListExtendedDto Add(TestListType type, IDto item)
        {
            try
            {
                switch (type)
                {
                    case TestListType.MedTest:
                        MedTests = MedTests.Append(item as MedTestDto);
                        break;
                    case TestListType.LabTest:
                        LabTests = LabTests.Append(item as LabTestDto);
                        break;
                    case TestListType.Examination:
                        Examinations = Examinations.Append(item as ExaminationDto);
                        break;
                    case TestListType.TestPanel:
                        TestPanels = TestPanels.Append(item as TestPanelDto);
                        break;
                    case TestListType.LabTestPanel:
                        LabTestPanels = LabTestPanels.Append(item as LabTestPanelDto);
                        break;

                    default: throw new Exception("Wrong type");
                }

                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TestListExtendedDto Add(MedTestDto item) => this.Add(TestListType.MedTest, item);
        public TestListExtendedDto Add(LabTestDto item) => this.Add(TestListType.LabTest, item);
        public TestListExtendedDto Add(ExaminationDto item) => this.Add(TestListType.Examination, item);
        public TestListExtendedDto Add(TestPanelDto item) => this.Add(TestListType.TestPanel, item);
        public TestListExtendedDto Add(LabTestPanelDto item) => this.Add(TestListType.LabTestPanel, item);

        public IDto Get(TestListType type, int id)
        {
            try
            {
                // check id exists?
                return type switch
                {
                    TestListType.MedTest =>
                        MedTests.FirstOrDefault(p => p.Id == id),
                    TestListType.LabTest =>
                        LabTests.FirstOrDefault(p => p.Id == id),
                    TestListType.Examination =>
                        Examinations.FirstOrDefault(p => p.Id == id),
                    TestListType.TestPanel =>
                        TestPanels.FirstOrDefault(p => p.Id == id),
                    TestListType.LabTestPanel =>
                        LabTestPanels.FirstOrDefault(p => p.Id == id),

                    _ => throw new Exception("Wrong type"),
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        // public IDto GetIndex(int index)
        // {
        //     try
        //     {
        //         KeyValuePair<TestListType, int> keyValue = this._index.ElementAt(index);

        //         return this.Get(keyValue.Key, keyValue.Value);
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        // }
    }
}