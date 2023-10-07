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
        $id = 1;
        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Đặc điểm chung của thực vật là gì ?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Lá của nhóm cây nào sau đây thuộc loại lá đơn ?";
        $question->save();
        
        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = " Những điều kiện bên ngoài nào ảnh hưỏng đến sự thoát hơi nước qua lá ?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Ở rễ, miền có chức năng dẫn truyền là?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Những đặc điểm nào của phiến lá phù hợp với việc thu nhận ánh sáng để quang hợp?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Thân cây gỗ to ra do?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Loại rễ biến dạng có chức năng lấy thức ăn từ cây chủ là?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Ở thực vật hô hấp xảy ra?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Phần lớn nước do rễ hút vào được thải ra ngoài qua?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Ở thực vật có 2 loại rễ chính là?";
        $question->save();

        $id++;

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Nhóm quả và hạt nào sau đây thích nghi với cách phát tán nhờ động vật?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Củ nào dưới đây không phải do thân biến dạng tạo ra ?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Loại rễ nào có chức năng chứa chất dự trữ cho cây dùng khi ra hoa, tạo quả ?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Trong những nhóm sau đây, nhóm nào toàn là cây có rễ cọc ?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Trong trồng cây, việc tĩa cành có ý nghĩa gì ?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Thân cây dài ra do?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Nguyên liệu lá cây sử dụng để chế tạo tinh bột là?";
        $question->save();
        
        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Sản phẩm được tạo ra từ quang hợp là?";
        $question->save();

        $question = new Question;
        $question->question_collection_id = $id;
        $question->content = "Có chức năng vận chuyển chất hữu cơ là?";
        $question->save();
    }
}