/* Наполнение базы данными */
INSERT INTO employee VALUES
(gen_random_uuid(), 'Дмитрий', 'Дмитриев', floor(random() * 10^4), now() - interval '1 month 1 day', floor(random() * 10^4), floor(random() * 10^3), 'Контракт Текст'),
(gen_random_uuid(), 'Иван', 'Иванов', floor(random() * 10^4), now() - interval '1 month 1 day', floor(random() * 10^4), floor(random() * 10^3), 'Контракт Текст'),
(gen_random_uuid(), 'Петр', 'Петров', floor(random() * 10^4), now() - interval '1 month 1 day', floor(random() * 10^4), floor(random() * 10^3), 'Контракт Текст');

INSERT INTO client VALUES
('36d6f926-3bb3-46f8-8cc1-bac1b90533de', 'Иван', 'Петров', floor(random() * 10^4), floor(random() * 10^4), now() - interval '1 month 1 day'),
('e72b3620-8d11-4e8e-b38b-ccfa46a7034f', 'Дмитрий', 'Дмитриев', floor(random() * 10^4), floor(random() * 10^4), now() - interval '1 month 1 day'),
(gen_random_uuid(), 'Иван', 'Иванов', floor(random() * 10^4), floor(random() * 10^4), now() - interval '1 month 1 day'),
(gen_random_uuid(), 'Петр', 'Петров', floor(random() * 10^4), floor(random() * 10^4), now() - interval '1 month 1 day');

INSERT INTO account VALUES
('RUB', 5000, gen_random_uuid(), '36d6f926-3bb3-46f8-8cc1-bac1b90533de'),
('USD', 10, gen_random_uuid(), '36d6f926-3bb3-46f8-8cc1-bac1b90533de'),
('EUR', 100, gen_random_uuid(), '36d6f926-3bb3-46f8-8cc1-bac1b90533de'),
('RUB', 1000, gen_random_uuid(), 'e72b3620-8d11-4e8e-b38b-ccfa46a7034f'),
('USD', 50, gen_random_uuid(), 'e72b3620-8d11-4e8e-b38b-ccfa46a7034f'),
('EUR', 2000, gen_random_uuid(), 'e72b3620-8d11-4e8e-b38b-ccfa46a7034f');

/* Выборка клиентов со счетом ниже определенного значения 
в порядке возрастания */
SELECT amount, client_id FROM account
WHERE (amount < 1000)
ORDER BY amount ASC;

/* Клиент с минимальной суммой на счете */
SELECT min(amount), client_id FROM account
GROUP BY client_id
LIMIT 1;

/* Подсчет суммы денег у всех клиентов */
SELECT sum(amount) FROM account;

/* Выборка банковских счетов и их владельцев */
SELECT * FROM account
LEFT JOIN client on account.client_id = client.client_id;

/* Клиенты от самых старших к самым младшим */
SELECT * FROM client
ORDER BY birthday_date ASC;

/* Сгруппировать клиентов по возрасту */
SELECT (current_date - birthday_date)/365 as age,
       COUNT(*) as d FROM client
GROUP BY age;

/* Вывод определенного числа человек из таблицы */
SELECT * FROM client
LIMIT 2;