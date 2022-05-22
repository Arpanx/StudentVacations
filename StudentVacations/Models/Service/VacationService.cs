namespace StudentVacations.Models.Service
{
    public class VacationService
    {
        public Сourse TryAddVacationItem(Vacation vacation, List<Сourse> courses)
        {
            var studentId = vacation.StudentId;
            var weekNumberStart = vacation.WeekNumberStart;
            var weekNumberEnd = vacation.WeekNumberEnd;

            //var courses = _db.Сourses.Where(x => x.StudentId == studentId)
            //    .AsNoTracking()
            //    .ToList();

            // Ищем вот такую ситуацию
            //  |------|        Курс    3-6
            //     |------|     Отпуск  4-7
            var items = courses.Where(x => weekNumberStart <= x.WeekNumberEnd)
                .Where(x => weekNumberEnd > x.WeekNumberEnd)
                .ToList();

            if (items?.Any() ?? false)
            {
                string message = "Бизнес-правило #1 Отпуск не может закачиваться на дату больше даты окончания курса";
                
                throw new Exception(message);
            }

            // Ищем вот такую ситуацию
            //  |------|    Курс    3-6
            //    |---|     Отпуск  4-5
            var items2 = courses.Where(x => weekNumberStart <= x.WeekNumberEnd)
                .Where(x => weekNumberEnd > x.WeekNumberStart)
                .ToList();

            if (!items2?.Any() ?? true)
            {
                // Если таких ситуаций нет, то делать нечего.
                string message = "Бизнес-правило #2 Отпуск должен пересекаться с каким либо курсом.";
                
                throw new Exception(message);
            }

            if (items2.Count() > 1)
            {
                string message = "Бизнес-правило #3 Отпуск не может пресекаться с более чем одним курсом";

                throw new Exception(message);
            }

            // Определяем дата начала отпуска внутри периода или снаружи
            var courseCurrent = items2.First();
            int diffStart = courseCurrent.WeekNumberStart - weekNumberStart;
            if (diffStart < 0)
            {
                // Дата начала отпуска больше даты начала курса (внутри периода). Значит нужно увеличить курс на всю длину отпуска
                int vacationsLenght = weekNumberEnd - weekNumberStart;  // Срок вакансии в неделю
                if (vacationsLenght < 0)
                {
                    string message = "Бизнес-правило #4 Дата окончания отпуска должна быть больше даты начала этого отпуска";

                    throw new Exception(message);
                }
                else
                {
                    // Потенциально новый срок окончания курса
                    courseCurrent.WeekNumberEnd = courseCurrent.WeekNumberEnd + vacationsLenght;
                }

                // Ищем все курсы студента кроме нашей текущей
                var courses2 = courses.Where(x => x.StudentId == studentId)
                    .Where(x => x.Id != courseCurrent.Id)
                    .ToList();

                // Добавляем потенциально отредактированный курс
                courses2.Add(courseCurrent);

                // Проверяем есть ли перекрытия по срокам
                bool overlap = courses2
                   .Any(r => courses2
                        .Where(q => q != r)
                        .Any(q => q.WeekNumberEnd >= r.WeekNumberStart && q.WeekNumberStart <= r.WeekNumberEnd));

                if (overlap)
                {
                    string message = "Бизнес-правило #5 Перенос срока окончания курса конфликтует с другими курсами";

                    throw new Exception(message);
                }

                // Все Ок, можно добавлять Отпуск и вот отредактированная версия курса который нужно обновить
                
                return courseCurrent;
            }
            else
            {
                // Дата начала отпуска меньше даты начала курса (перекрытие периода). Значит нужно увеличить курс на часть длины отпуска
                // Вот такая ситуация
                //    |------|    Курс    3-6
                // |------|       Отпуск  2-5
                int vacationsLenght = weekNumberEnd - courseCurrent.WeekNumberStart;  // Срок вакансии в неделю
                if (vacationsLenght < 0)
                {
                    string message = "Бизнес-правило #4 Дата окончания отпуска должна быть больше даты начала этого отпуска";

                    throw new Exception(message);
                }
                else
                {
                    // Потенциально новый срок окончания курса
                    courseCurrent.WeekNumberEnd = courseCurrent.WeekNumberEnd + vacationsLenght;
                }

                // Ищем все курсы студента кроме нашей текущей
                var courses2 = courses.Where(x => x.StudentId == studentId)
                    .Where(x => x.Id != courseCurrent.Id)
                    .ToList();

                // Добавляем потенциально отредактированный курс
                courses2.Add(courseCurrent);

                // Проверяем есть ли перекрытия по срокам
                bool overlap = courses2
                   .Any(r => courses2
                        .Where(q => q != r)
                        .Any(q => q.WeekNumberEnd >= r.WeekNumberStart && q.WeekNumberStart <= r.WeekNumberEnd));

                if (overlap)
                {
                    string message = "Бизнес-правило #5 Перенос срока окончания курса конфликтует с другими курсами";

                    throw new Exception(message);
                }

                return courseCurrent;

            }

            return null;
        }
    }
}
