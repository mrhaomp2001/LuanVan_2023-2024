<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\Answer;

class AnswerSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        for ($i = 1; $i <= 300; $i++) {
            for ($j = 1; $j <= 4; $j++) {
                $answer = new Answer;
                $answer->question_id = $i;
                $answer->content = 'Câu trả lời ' . $j . ' - của câu hỏi ' . $i;
                
                if ($j == 4) {
                    $answer->is_correct = true;
                } else {
                    $answer->is_correct = false;
                }

                $answer->save();
            }
        }
    }
}