-- Inserting 9 random blog entries
INSERT INTO Blogs
  (Name, Description, Author, Date, ImageName, CategoryId)
VALUES
  ('Tech Innovations', 'Exploring the latest in technology and innovation.', 'John Doe', '2023-06-12', 'blog1.jpg', 1),
  ('Healthy Living', 'Tips and tricks for a healthy lifestyle.', 'Jane Smith', '2022-07-01', 'blog2.jpg', 1),
  ('Travel Diaries', 'Experiences and stories from around the world.', 'Emily Davis', '2023-02-18', 'blog3.jpg', 2),
  ('Cooking with Love', 'Delicious recipes and cooking tips.', 'Michael Brown', '2024-04-10', 'blog4.jpg', 3),
  ('Fitness Journey', 'My personal fitness journey and workout routines.', 'Sarah Wilson', '2022-09-01', 'blog5.jpg', 4),
  ('Mindfulness Matters', 'Practicing mindfulness and meditation techniques.', 'David Johnson', '2023-05-04', 'blog6.jpg', 4),
  ('DIY Crafts', 'Fun and creative DIY craft projects.', 'Emma White', '2024-03-07', 'blog7.jpg', 5),
  ('Book Reviews', 'Reviewing the latest books in various genres.', 'Liam Garcia', '2023-12-20', 'blog8.jpg', 5),
  ('Gardening Tips', 'Growing and caring for your garden.', 'Olivia Martinez', '2023-10-17', 'blog9.jpg', 6);


-- Inserting 3 random feature entries
INSERT INTO Features
  (Title, Description)
VALUES
  ('New User Interface', 'The latest update includes a brand-new user interface with improved navigation and aesthetics.'),
  ('Enhanced Security', 'Our new security features protect your data with advanced encryption and multi-factor authentication.'),
  ('Performance Improvements', 'Experience faster load times and more responsive interactions with our performance enhancements.');


-- Inserting 10 random notice entries
INSERT INTO Notices
  (Topic, Date)
VALUES
  ('Scheduled Maintenance', '2024-06-01'),
  ('New Feature Release', '2024-06-05'),
  ('Holiday Announcement', '2024-06-15'),
  ('Security Update', '2024-06-20'),
  ('System Downtime', '2024-06-25'),
  ('User Feedback Survey', '2024-06-30'),
  ('Policy Update', '2024-07-05'),
  ('Training Session', '2024-07-10'),
  ('Community Event', '2024-07-15'),
  ('Product Launch', '2024-07-20');


-- Inserting 10 random tag entries
INSERT INTO Tags
  (Name)
VALUES
  ('Technology'),
  ('Health'),
  ('Travel'),
  ('Food'),
  ('Education'),
  ('Lifestyle'),
  ('Finance'),
  ('Sports'),
  ('Entertainment'),
  ('Science');


-- Inserting 3 random slider entries
INSERT INTO Sliders
  (Title, Description, BtnText, BtnUrl, ImageName, [Order])
VALUES
  ('Welcome to Our Website', 'Discover the latest features and updates.', 'Learn More', 'https://example.com/learn-more', 'slider1.jpg', 1),
  ('Join Our Community', 'Be part of our growing community.', 'Sign Up', 'https://example.com/sign-up', 'slider2.jpg', 2),
  ('Explore Our Services', 'Find out more about what we offer.', 'Get Started', 'https://example.com/get-started', 'slider3.jpg', 3);


-- Inserting 6 random category entries
INSERT INTO Categories
  (Name)
VALUES
  ('Travel'),
  ('Lifestyle'),
  ('Finance'),
  ('Sports'),
  ('Entertainment'),
  ('Science');


-- Inserting 9 random teacher entries
INSERT INTO Teachers
  (FullName, Position, About, Degree, Experience, Hobbies, Faculty, Mail, Phone, Skype, ImageName)
VALUES
  ('John Doe', 'Professor', 'Expert in Computer Science with over 20 years of experience.', 'PhD in Computer Science', '20 years', '["Reading", "Cycling"]', 'Engineering', 'john.doe@example.com', '123-456-7890', 'john.doe', 'teacher1.jpg'),
  ('Jane Smith', 'Associate Professor', 'Specializes in software engineering and project management.', 'MSc in Software Engineering', '15 years', '["Traveling", "Cooking"]', 'Engineering', 'jane.smith@example.com', '234-567-8901', 'jane.smith', 'teacher2.jpg'),
  ('Emily Johnson', 'Lecturer', 'Focused on data science and machine learning applications.', 'MSc in Data Science', '10 years', '["Hiking", "Photography"]', 'Science', 'emily.johnson@example.com', '345-678-9012', 'emily.johnson', 'teacher3.jpg'),
  ('Michael Brown', 'Assistant Professor', 'Experienced in network security and cyber defense.', 'PhD in Cybersecurity', '12 years', NULL, 'Information Technology', 'michael.brown@example.com', '456-789-0123', 'michael.brown', 'teacher4.jpg'),
  ('Sarah Wilson', 'Senior Lecturer', 'Passionate about teaching artificial intelligence and robotics.', 'PhD in Artificial Intelligence', '18 years', '["Gardening", "Writing"]', 'Robotics', 'sarah.wilson@example.com', '567-890-1234', 'sarah.wilson', 'teacher5.jpg'),
  ('David Martinez', 'Professor', 'Leader in bioinformatics and computational biology research.', 'PhD in Bioinformatics', '22 years', '["Chess", "Swimming"]', 'Biology', 'david.martinez@example.com', '678-901-2345', 'david.martinez', 'teacher6.jpg'),
  ('Laura Garcia', 'Lecturer', 'Focuses on environmental science and sustainability.', 'MSc in Environmental Science', '8 years', NULL, 'Environmental Studies', 'laura.garcia@example.com', '789-012-3456', 'laura.garcia', 'teacher7.jpg'),
  ('James Anderson', 'Assistant Professor', 'Expertise in renewable energy and green technologies.', 'PhD in Renewable Energy', '14 years', '["Painting", "Music"]', 'Energy', 'james.anderson@example.com', '890-123-4567', 'james.anderson', 'teacher8.jpg'),
  ('Olivia Martinez', 'Senior Lecturer', 'Specialist in psychology and human behavior studies.', 'PhD in Psychology', '16 years', '["Yoga", "Meditation"]', 'Psychology', 'olivia.martinez@example.com', '901-234-5678', 'olivia.martinez', 'teacher9.jpg'),
  ('Alexander Hamilton', 'Visiting Professor', 'Expert in political science with extensive experience in public policy.', 'PhD in Political Science', '25 years', '["Reading", "Debating"]', 'Political Science', 'alex.hamilton@example.com', '111-222-3333', 'alex.hamilton', 'teacher10.jpg'),
  ('Charlotte Bronte', 'Assistant Lecturer', 'Specializes in 19th-century literature and creative writing.', 'MA in English Literature', '7 years', '["Writing", "Hiking"]', 'Humanities', 'charlotte.bronte@example.com', '444-555-6666', 'charlotte.bronte', 'teacher11.jpg'),
  ('Nikola Tesla', 'Research Fellow', 'Renowned for contributions to electrical engineering and wireless communications.', 'PhD in Electrical Engineering', '30 years', '["Inventing", "Exploring"]', 'Engineering', 'nikola.tesla@example.com', '777-888-9999', 'nikola.tesla', 'teacher12.jpg');


-- Inserting 12 random event entries
INSERT INTO Events
  (Name, Description, StartDate, EndDate, Venue, ImageName, CategoryId)
VALUES
  ('Tech Conference 2024', 'A conference discussing the latest advancements in technology.', '2024-08-01 09:00:00', '2024-08-01 17:00:00', 'Tech Convention Center', 'event1.jpg', 1),
  ('Health & Wellness Expo', 'An expo focused on health, wellness, and fitness.', '2024-08-15 10:00:00', '2024-08-15 18:00:00', 'City Expo Center', 'event2.jpg', 2),
  ('Travel Fair 2024', 'A fair showcasing travel destinations and packages.', '2024-09-05 11:00:00', '2024-09-05 19:00:00', 'Downtown Exhibition Hall', 'event3.jpg', 3),
  ('Food Festival', 'A festival celebrating cuisine from around the world.', '2024-09-20 12:00:00', '2024-09-20 20:00:00', 'Central Park', 'event4.jpg', 4),
  ('Education Summit', 'A summit on the future of education and learning.', '2024-10-10 09:30:00', '2024-10-10 17:30:00', 'University Auditorium', 'event5.jpg', 5),
  ('Finance Seminar', 'A seminar on financial planning and investment strategies.', '2024-11-05 10:00:00', '2024-11-05 16:00:00', 'Financial District Hall', 'event6.jpg', 6),
  ('Sports Meet 2024', 'An event bringing together sports enthusiasts and professionals.', '2024-11-20 08:00:00', '2024-11-20 18:00:00', 'Sports Arena', 'event7.jpg', 1),
  ('Entertainment Gala', 'A gala celebrating the entertainment industry.', '2024-12-01 19:00:00', '2024-12-01 23:00:00', 'Grand Ballroom', 'event8.jpg', 2),
  ('Science Symposium', 'A symposium discussing recent developments in science.', '2024-12-15 09:00:00', '2024-12-15 17:00:00', 'Research Institute', 'event9.jpg', 3),
  ('Tech Innovations Forum', 'A forum to discuss recent innovations in technology.', '2025-01-12 09:00:00', '2025-01-12 17:00:00', 'Tech Expo Hall', 'event10.jpg', 4),
  ('Digital Marketing Summit', 'Summit on the latest trends in digital marketing.', '2025-01-18 10:00:00', '2025-01-18 18:00:00', 'Marketing Conference Center', 'event11.jpg', 5),
  ('AI & Robotics Expo', 'Expo showcasing advancements in AI and robotics.', '2025-02-03 09:00:00', '2025-02-03 17:00:00', 'AI Convention Hall', 'event12.jpg', 6);


-- Inserting 9 random course entries
INSERT INTO Courses
  (Name, Description, About, HowToApply, Certification, StartDate, Duration, ClassDuration, SkillLevel, Language, StudentCount, Price, ImageName, CategoryId, IsDeleted)
VALUES
  ('Travel Photography Basics', 'Learn the art of travel photography.', 'This course covers techniques for capturing stunning travel photos.', 'Apply through our website.', 'Photography Certificate', '2024-06-01', 30, 2, 0, 'English', 50, 199.99, 'course1.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Travel'), 0),
  ('Travel Blogging 101', 'Start your own travel blog.', 'Learn how to create, maintain, and monetize a travel blog.', 'Fill out the online application form.', 'Blogging Certificate', '2024-07-01', 45, 2, 0, 'English', 60, 149.99, 'course2.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Travel'), 0),
  ('Personal Finance Management', 'Master the basics of personal finance.', 'This course covers budgeting, saving, and investing.', 'Register online.', 'Finance Certificate', '2024-08-01', 30, 2, 1, 'English', 100, 99.99, 'course3.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Finance'), 0),
  ('Investment Strategies', 'Learn advanced investment strategies.', 'Focus on stocks, bonds, and other investment vehicles.', 'Submit your application online.', 'Investment Certificate', '2024-09-01', 60, 3, 2, 'English', 80, 299.99, 'course4.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Finance'), 0),
  ('Healthy Living & Wellness', 'Improve your health and wellness.', 'Learn about nutrition, exercise, and mental health.', 'Apply on our website.', 'Wellness Certificate', '2024-10-01', 40, 2, 0, 'English', 90, 129.99, 'course5.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Lifestyle'), 0),
  ('Fitness Training', 'Become a certified fitness trainer.', 'This course covers exercise science and training techniques.', 'Enroll through our online portal.', 'Fitness Trainer Certificate', '2024-11-01', 35, 2, 1, 'English', 100, 249.99, 'course6.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Sports'), 0),
  ('Sports Management', 'Learn the fundamentals of sports management.', 'Focus on sports marketing, administration, and event planning.', 'Sign up online.', 'Sports Management Certificate', '2024-12-01', 50, 3, 1, 'English', 60, 299.99, 'course7.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Sports'), 0),
  ('Film Production Basics', 'Get started with film production.', 'Learn about scriptwriting, shooting, and editing.', 'Apply via our website.', 'Film Production Certificate', '2024-06-15', 25, 2, 0, 'English', 70, 199.99, 'course8.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Entertainment'), 0),
  ('Introduction to Astrophysics', 'Explore the fundamentals of astrophysics.', 'Learn about stars, galaxies, and the universe.', 'Register online.', 'Astrophysics Certificate', '2024-07-15', 45, 3, 2, 'English', 80, 399.99, 'course9.jpg', (SELECT Id
    FROM Categories
    WHERE Name = 'Science'), 0);


-- Inserting 18 random courseTag entries
INSERT INTO CourseTags
  (CourseId, TagId)
VALUES
  (1, 1),
  (1, 10),
  (2, 1),
  (2, 5),
  (3, 2),
  (3, 6),
  (4, 3),
  (4, 7),
  (5, 3),
  (5, 8),
  (6, 4),
  (6, 9),
  (7, 4),
  (7, 10),
  (8, 5),
  (8, 2),
  (9, 6),
  (9, 1);


-- Inserting 18 random eventTag entries
INSERT INTO EventTags
  (EventId, TagId)
VALUES
  (1, 1),
  (1, 10),
  (2, 1),
  (2, 5),
  (3, 2),
  (3, 6),
  (4, 3),
  (4, 7),
  (5, 3),
  (5, 8),
  (6, 4),
  (6, 9),
  (7, 4),
  (7, 10),
  (8, 5),
  (8, 2),
  (9, 6),
  (9, 1),
  (10, 5),
  (10, 8),
  (11, 4),
  (11, 1),
  (12, 2),
  (12, 7);


-- Inserting 18 random blogTag entries
INSERT INTO BlogTags
  (BlogId, TagId)
VALUES
  (10, 1),
  (10, 10),
  (2, 1),
  (2, 5),
  (3, 2),
  (3, 6),
  (4, 3),
  (4, 7),
  (5, 3),
  (5, 8),
  (6, 4),
  (6, 9),
  (7, 4),
  (7, 10),
  (8, 5),
  (8, 2),
  (9, 6),
  (9, 1);


-- Inserting data into the EventTeachers table
INSERT INTO EventTeachers
  (EventId, TeacherId)
VALUES
  (1, 1),
  (1, 2),
  (2, 3),
  (2, 4),
  (3, 4),
  (3, 5),
  (4, 5),
  (4, 6),
  (5, 6),
  (5, 7),
  (6, 1),
  (6, 2),
  (7, 8),
  (7, 9),
  (8, 9),
  (8, 1),
  (9, 7),
  (9, 2),
  (10, 7),
  (10, 3),
  (11, 4),
  (11, 5),
  (12, 6),
  (12, 8);


-- Inserting data into the Icons table
INSERT INTO Icons
  (Name, Url)
VALUES
  ('zmdi zmdi-facebook', 'https://facebook.com/teacher1'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher1'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher1'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher1'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher2'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher2'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher2'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher2'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher3'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher3'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher3'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher3'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher4'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher4'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher4'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher4'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher5'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher5'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher5'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher5'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher6'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher6'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher6'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher6'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher7'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher7'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher7'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher7'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher8'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher8'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher8'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher8'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher9'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher9'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher9'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher9'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher10'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher10'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher10'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher10'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher11'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher11'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher11'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher11'),

  ('zmdi zmdi-facebook', 'https://facebook.com/teacher12'),
  ('zmdi zmdi-vimeo', 'https://vimeo.com/teacher12'),
  ('zmdi zmdi-pinterest', 'https://pinterest.com/teacher12'),
  ('zmdi zmdi-twitter', 'https://twitter.com/teacher12');


-- Inserting data into the TeacherIcons table
INSERT INTO TeacherIcons
  (TeacherId, IconId)
VALUES
  (1, 1),
  (1, 2),
  (1, 3),
  (1, 4),
  (2, 5),
  (2, 6),
  (2, 7),
  (2, 8),
  (3, 9),
  (3, 10),
  (3, 11),
  (3, 12),
  (4, 13),
  (4, 14),
  (4, 15),
  (4, 16),
  (5, 17),
  (5, 18),
  (5, 19),
  (5, 20),
  (6, 21),
  (6, 22),
  (6, 23),
  (6, 24),
  (7, 25),
  (7, 26),
  (7, 27),
  (7, 28),
  (8, 29),
  (8, 30),
  (8, 31),
  (8, 32),
  (9, 33),
  (9, 34),
  (9, 35),
  (9, 36),
  (10, 37),
  (10, 38),
  (10, 39),
  (10, 40),
  (11, 41),
  (11, 42),
  (11, 43),
  (11, 44),
  (12, 45),
  (12, 46),
  (12, 47),
  (12, 48);


-- Inserting data into the Skills table
INSERT INTO Skills
  (Name, [Percent])
VALUES
  ('Python', 90),
  ('Data Analysis', 85),
  ('Machine Learning', 80),
  ('Deep Learning', 75),
  ('SQL', 70),
  ('Statistics', 65),
  ('JavaScript', 95),
  ('React', 90),
  ('Node.js', 85),
  ('Express.js', 80),
  ('MongoDB', 75),
  ('GraphQL', 70),
  ('Java', 88),
  ('Spring Boot', 85),
  ('Hibernate', 82),
  ('Microservices', 80),
  ('Docker', 78),
  ('Kubernetes', 75),
  ('HTML', 100),
  ('CSS', 95),
  ('Bootstrap', 85),
  ('Responsive Design', 80),
  ('SEO', 75),
  ('C#', 92),
  ('ASP.NET', 88),
  ('Entity Framework', 85),
  ('Azure', 80),
  ('Blazor', 78),
  ('Xamarin', 75),
  ('Project Management', 90),
  ('Agile', 85),
  ('Scrum', 80),
  ('Kanban', 75),
  ('Risk Management', 70),
  ('Communication', 95),
  ('Creative Writing', 95),
  ('Editing', 90),
  ('Storytelling', 85),
  ('Grammar', 80),
  ('Proofreading', 75),
  ('Research', 70),
  ('Public Speaking', 100),
  ('Presentation Skills', 95),
  ('Body Language', 90),
  ('Voice Modulation', 85),
  ('Negotiation', 80),
  ('Conflict Resolution', 75),
  ('Physics', 95),
  ('Astrophysics', 90),
  ('Mathematics', 85),
  ('Programming', 80),
  ('Political Science', 95),
  ('Public Policy', 90),
  ('Debating', 85),
  ('English Literature', 95),
  ('Creative Writing', 90),
  ('Inventing', 98),
  ('Electrical Engineering', 95),
  ('Wireless Communications', 90);


-- Inserting data into the TeacherSkills table
INSERT INTO TeacherSkills
  (TeacherId, SkillId)
VALUES
  (1, 1),
  (1, 2),
  (1, 3),
  (1, 4),
  (1, 5),
  (1, 6),
  (2, 7),
  (2, 8),
  (2, 9),
  (2, 10),
  (2, 11),
  (2, 12),
  (3, 13),
  (3, 14),
  (3, 15),
  (3, 16),
  (3, 17),
  (3, 18),
  (4, 19),
  (4, 20),
  (4, 7),
  (4, 21),
  (4, 22),
  (4, 23),
  (5, 24),
  (5, 25),
  (5, 26),
  (5, 27),
  (5, 28),
  (5, 29),
  (6, 30),
  (6, 31),
  (6, 32),
  (6, 33),
  (6, 34),
  (6, 35),
  (7, 36),
  (7, 37),
  (7, 38),
  (7, 39),
  (7, 40),
  (7, 41),
  (8, 42),
  (8, 43),
  (8, 44),
  (8, 45),
  (8, 46),
  (8, 47),
  (9, 48),
  (9, 49),
  (9, 50),
  (9, 51),
  (9, 2),
  (9, 41),
  (10, 52),
  (10, 53),
  (10, 54),
  (11, 55),
  (11, 56),
  (12, 57),
  (12, 58),
  (12, 59);


-- Inserting 3 random post entries
INSERT INTO Posts
  (Name, Author, Date, ImageName)
VALUES
  ('Understanding C#', 'JohnDoe', '2023-03-15', 'post1.jpg'),
  ('Exploring .NET', 'JaneSmith', '2023-04-20', 'post2.jpg'),
  ('Advanced ASP.NET', 'MarkTwain', '2023-05-10', 'post3.jpg');


-- Inserting 3 random testimonial entries
INSERT INTO Testimonials
  (Text, Author, Position, ImageName, [Order])
VALUES
  ('This platform has significantly boosted my career. The courses are comprehensive and up-to-date.', 'John Doe', 'Software Engineer', 'testimonial1.jpg', 1),
  ('The learning experience here is fantastic! The instructors are very knowledgeable.', 'Jane Smith', 'Data Scientist', 'testimonial2.jpg', 2),
  ('I highly recommend this to anyone looking to advance their skills. It has been a game changer for me.', 'Alice Johnson', 'Product Manager', 'testimonial3.jpg', 3);


-- Inserting 3 random branch entries
  INSERT INTO Branches (Title, Address1, Address2, ImageName)
VALUES 
('BranchTitle1', '123 Main St', 'Suite 100', 'contact1.png'),
('BranchTitle2', '456 Elm St', 'Suite 200', 'contact2.png'),
('BranchTitle3', '789 Oak St', 'Suite 300', 'contact3.png');

