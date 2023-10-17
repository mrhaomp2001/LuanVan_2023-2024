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
        $id = 1;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Tự tổng hợp chất hữu cơ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Phần lớn không có khả năng di chuyển";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Phản ứng chậm với các kích thích từ bên ngoài";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Tất cả đều đúng";
        $answer->is_correct = true;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Cây ổi, cây bàng, cây mướp";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Cây cau, cây su hào, cây hoa sữa";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Cây đào, cây cải, cây xấu hổ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Cây bí, cây me, cây xoài";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = " Ánh sáng, diệp lục";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Độ ẩm của không khí";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Ánh sáng, nhiệt độ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Độ ẩm của không khí, ánh sáng, nhiệt độ";
        $answer->is_correct = true;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Miền sinh trưởng";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Miền trưởng thành";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Miền hút";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Miền chóp rễ";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Biểu bì trong suốt ở hai mặt của phiến lá";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Tế bào thịt lá có vách mỏng, nhiều lục lạp";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Xen giữa tế bào thịt lá phía dưới là những khoảng trống";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Biểu bì trong suốt ở hai mặt của phiến lá và Tế bào thịt lá có vách mỏng, nhiều lục lạp";
        $answer->is_correct = true;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Sự phân chia tế bào ở mô phân sinh ngọn";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Sự phân chia tế bào ở tầng sinh vỏ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Sự phân chia tế bào ở tầng sinh trụ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Sự phân chia tế bào ở tầng sinh trụ và tầng sinh vỏ";
        $answer->is_correct = true;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ củ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ móc";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ thở";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ Giác mút";
        $answer->is_correct = true;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Mọi lúc, khi cây còn sống";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Buổi sáng";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Buổi chiều";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Buổi tối";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Lá";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Gân lá";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Lỗ khí của lá";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Thân, cành lá";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ cái và rễ con";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ cọc và rễ chùm";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ cọc vầ rề cái";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ chùm và rễ con";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Những quả và hạt nhẹ thường có cánh hoặc có túm lông";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Vỏ quả có khả năng tự tách hoặc mở ra để hạt tung ra ngoài";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Những quả và hạt có nhiều gai hoặc móc, hoặc làm thức ăn cho động vật";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Tất cả đều đúng";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Củ khoai tây";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Củ cà rốt";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Củ gừng";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Củ dong ta";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ thở";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ móc";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ củ";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Rễ cọc";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Cây xoài, cây mít, cây đậu";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Cây bưởi, cây đậu, cây hành";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Cây mít, cây cải, cây lúa";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Cây hành, cây ngô, cây lúa";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = " Loại bỏ các cành sâu, xấu";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Giúp cây mọc thẳng";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Để các cành còn lại phát triển tốt hơn";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Loại bỏ các cành sâu, xấu và giúp cây mọc thẳng";
        $answer->is_correct = true;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "sự lớn lên và phân chia tế bào";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "chồi ngọn";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "mô phân sinh ngọn";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "sự phân chia tế bào ở mô phân sinh ngọn";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Khí ôxi và nước";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Khí cacbônic và muối khoáng";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Nước và khí cacbônic";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Khi ôxi, nước và muối khoáng";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Tinh bột ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Khí ôxi";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Tinh bột và Khí ôxi";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Khí cacbônic";
        $answer->is_correct = false;
        $answer->save();

        /// New Answers
        $id++;
        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Mạch gỗ ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Mạch rây";
        $answer->is_correct = true;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Vỏ";
        $answer->is_correct = false;
        $answer->save();

        $answer = new Answer;
        $answer->question_id = $id;
        $answer->content = "Trụ giữa";
        $answer->is_correct = false;
        $answer->save();
    }
}