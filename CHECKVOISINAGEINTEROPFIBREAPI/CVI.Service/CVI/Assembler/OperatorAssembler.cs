using CVI.Domain.Model;
using CVI.Service.CVI.DTO;

namespace CVI.Service.CVI.Assembler
{
    /// <summary>
    /// The OperatorAssembler class
    /// </summary>
    public static class OperatorAssembler
    {
        /// <summary>
        /// From Operator to OperatorDto
        /// </summary>
        /// <param name="oprt"></param>
        /// <returns></returns>
        public static Operator ToOperator(this OperatorDTO oprtDto)
        {
            return new Operator
            {
                ID = oprtDto.ID,
                OperatorName = oprtDto.Name
            };
        }

        /// <summary>
        /// From Operator to OperatorDto
        /// </summary>
        /// <param name="oprt"></param>
        /// <returns></returns>
        public static OperatorDTO ToOperatorDto(this Operator oprt)
        {
            return new OperatorDTO
            {
                ID = oprt.ID,
                Name = oprt.OperatorName
            };
        }
    }
}
