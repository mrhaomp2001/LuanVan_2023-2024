<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Question;

class QuestionSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        for ($n = 1; $n <= 10; $n++) {
            for ($i = 1; $i <= 30; $i++) {
                $question = new Question;
                
                $question->question_collection_id = $n;
                $question->content = 'Câu hỏi thứ ' . $i . ' - Thuộc bộ đề: ' . $n . '.';
                $question->save();
            }
        }
    }
}