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

        User::create(
            [
                'name' => "admin",
                'username' => "admin",
                'password' => Hash::make("password"),
                'classroom_id' => "1",
            ]
        );

        $this->call(PostTemplateSeeder::class);
        $this->call(PostSeeder::class);
        $this->call(CommentSeeder::class);
        
        $this->call(ClassroomSeeder::class);
        $this->call(QuestionSeeder::class);
        $this->call(AnswerSeeder::class);
    }
}