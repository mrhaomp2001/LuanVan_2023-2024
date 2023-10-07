<?php

namespace Database\Seeders;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use App\Models\User;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Hash;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        // \App\Models\User::factory(10)->create();
        // \App\Models\User::factory()->create([
        //     'name' => 'Test User',
        //     'email' => 'test@example.com',
        // ]);

        for ($i = 1; $i < 10; $i++) {
            User::create(
                [
                    'name' => "admin" . $i,
                    'username' => "admin" . $i,
                    'password' => Hash::make("password"),
                ]
            );
        }


        $this->call(PostTemplateSeeder::class);
        $this->call(PostSeeder::class);
        $this->call(CommentSeeder::class);

        $this->call(ClassroomSeeder::class);
        $this->call(StudyClassroomSeeder::class);

        $this->call(QuestionCollectionSeeder::class);
        $this->call(QuestionSeeder::class);
        $this->call(AnswerSeeder::class);

        $this->call(NotificationTypeSeeder::class);
        $this->call(NotificationSeeder::class);

        $this->call(StudyDocumentSeeder::class);
        
        $this->call(ReportTypeSeeder::class);
    }
}